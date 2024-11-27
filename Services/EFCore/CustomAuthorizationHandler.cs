using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuthenticationService _authService;
        public CustomAuthorizationHandler(UserManager<User> userManager, IAuthenticationService authService, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _authService = authService;
            _roleManager = roleManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            var user = await _authService.GetUserByUserName(context.User.Identity.Name);

            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = new List<Claim>();
                foreach (var roleName in roles)
                {
                    var roleEntity = await _roleManager.FindByNameAsync(roleName);
                    if (roleEntity is not null)
                    {
                        var claims = await _roleManager.GetClaimsAsync(roleEntity);
                        roleClaims.AddRange(claims);
                    }
                }

                if (userClaims.Any(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue) ||
                    roleClaims.Any(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }
    }
}
