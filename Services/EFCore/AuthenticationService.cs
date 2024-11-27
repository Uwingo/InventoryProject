using AutoMapper;
using Entities.Exception;
using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class AuthenticationService:IAuthenticationService
        
    {
    
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;

            private User? _user;

            public AuthenticationService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, RoleManager<Role> roleManager)
            {
                _mapper = mapper;
                _configuration = configuration;
                _userManager = userManager;
                _roleManager = roleManager;

            }

            public async Task<IdentityResult> RegisterUser(UserForRegistrationDTO registrationUser)
            {
                var user = _mapper.Map<User>(registrationUser);
                var result = await _userManager.CreateAsync(user, registrationUser.Password);

                if (result.Succeeded) await _userManager.AddToRolesAsync(user, registrationUser.Roles);

                return result;
            }
            public async Task<User> GetUserByUserName(string userName)
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.UserName == userName);
                return user;
            }

            public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthDto)
            {
                _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
                var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
                if (!result) Console.WriteLine("Kullanıcı adı veya şifre hatalıdır.");
                return result;
            }

            public async Task<TokenDTO> CreateToken(bool populateExp)
            {
                var signinCredentials = GetSigninCredentials();
                var claims = await GetClaims();
                var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

                var refreshToken = GenerateRefreshToken();
                _user.RefreshToken = refreshToken;

                if (populateExp) _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                await _userManager.UpdateAsync(_user);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new TokenDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }


            private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["ValidateIssue"],
                    audience: jwtSettings["ValidateAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["Expire"])),
                    signingCredentials: signinCredentials);
                return tokenOptions;
            }

            private string GenerateRefreshToken()
            {
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);
                }
            }
            private async Task<List<Claim>> GetClaims()
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.Email, _user.Email)
            };

                var roles = await _userManager
                    .GetRolesAsync(_user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return claims;
            }

            public async Task<List<Claim>> GetClaimsByUser(User user)
            {
                // Kullanıcıya ait temel claim'leri ekle
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("uid", user.Id)
    };

                // UwingoUser oluştur
                var uwingoUser = await _userManager.FindByIdAsync(user.Id);
                if (uwingoUser is null)
                {
                    throw new InvalidOperationException("Kullanıcı bulunamadı.");
                }

                // Rolleri alın
                var roles = await _userManager.GetRolesAsync(uwingoUser);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    var usersRole = await _roleManager.FindByNameAsync(role);

                    if (usersRole is not null)
                    {
                        var roleClaims = await GetRoleClaimsAsync(usersRole.Id);
                        foreach (var roleClaim in roleClaims)
                        {
                            claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
                        }
                    }
                }

                // Kullanıcıya ait claim'leri alın
                var userClaims = await _userManager.GetClaimsAsync(uwingoUser);

                foreach (var userClaim in userClaims)
                {
                    claims.Add(new Claim(userClaim.Type, userClaim.Value));
                }

                return claims;
            }
            public async Task<IEnumerable<Claim>> GetRoleClaimsAsync(string roleId)
            {
                // İlk olarak birinci veritabanında rolü kontrol et
                var role = await _roleManager.FindByIdAsync(roleId);

                // Rol bulunduğunda claim'leri getir
                return await _roleManager.GetClaimsAsync(role);
            }

            private SigningCredentials GetSigninCredentials()
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
                var secret = new SymmetricSecurityKey(key);
                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            public async Task<TokenDTO> RefreshToken(TokenDTO tokenDto)
            {
                var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
                var user = await _userManager.FindByNameAsync(principal.Identity.Name);

                if (user is null ||
                    user.RefreshToken != tokenDto.RefreshToken ||
                    user.RefreshTokenExpiryTime <= DateTime.Now)

                    throw new RefreshTokenBadRequestException();

                _user = user;
                return await CreateToken(populateExp: false);
            }

            private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidateIssue"],
                    ValidAudience = jwtSettings["ValidateAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                    out securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken; //as gibi davranmasını sağlar(Başarısız olma durumunda null gelecek)
                if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token.");
                }
                return principal;
            }

        
    }
}
