using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class CustomAuthorizationService : ICustomAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public CustomAuthorizationService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> IsAuthorizedAsync(User user, string claimType, string claimValue)
        {
            var claims = (await _userManager.GetClaimsAsync(user)).ToList();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
                claims.AddRange(roleClaims);
            }

            // Check if the required claim is present
            return claims.Any(c => c.Type == claimType && c.Value == claimValue);
        }

        public async Task<IEnumerable<CustomRequirement>> GetCustomRequirementsAsync()
        {
            var requirements = new List<CustomRequirement>();

            var roles = await _roleManager.Roles.ToListAsync(); // Use async method for better performance
            var users = await _userManager.Users.ToListAsync();

            // Fetch role claims
            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                if (roleClaims != null && roleClaims.Any())
                {
                    requirements.AddRange(roleClaims.Select(claim => new CustomRequirement(claim.Type, claim.Value)));
                }
            }

            // Fetch user claims
            foreach (var user in users)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                if (userClaims != null && userClaims.Any())
                {
                    requirements.AddRange(userClaims.Select(claim => new CustomRequirement(claim.Type, claim.Value)));
                }
            }

            return requirements.Distinct(); // Ensure no duplicate requirements
        }

        public async Task<CustomRequirement> GetRequirementByPolicyNameAsync(string policyName)
        {
            // Burada veritabanından gelen verileri çekiyoruz
            var roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var claim = roleClaims.FirstOrDefault(c => c.Type == "permission" && c.Value == policyName);
                if (claim != null)
                {
                    return new CustomRequirement(claim.Type, claim.Value);
                }
            }

            return null;
        }
    }
}
