using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDTO userForRegistrationDto);
        Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthenticationDto);
        Task<TokenDTO> CreateToken(bool populateExp);
        Task<TokenDTO> RefreshToken(TokenDTO tokenDto);
        Task<User> GetUserByUserName(string userName);
        Task<List<Claim>> GetClaimsByUser(User user);
    }
}
