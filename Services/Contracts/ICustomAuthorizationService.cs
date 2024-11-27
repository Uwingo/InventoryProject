using Entities.Model;
using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICustomAuthorizationService
    {
        Task<bool> IsAuthorizedAsync(User user, string claimType, string claimValue);
        Task<IEnumerable<CustomRequirement>> GetCustomRequirementsAsync();
        Task<CustomRequirement> GetRequirementByPolicyNameAsync(string policyName);
    }
}
