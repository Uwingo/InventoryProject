using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace Inventory.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _serviceManager;
        private ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService serviceManagers, ILogger<AuthenticationController> logger)
        {
            _serviceManager = serviceManagers;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
        {
            if (!await _serviceManager.ValidateUser(user))
                return Unauthorized();
            var tokenDto = await _serviceManager.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserForRegistrationDTO appUserDTO)
        {
           
            var user = await _serviceManager.RegisterUser(appUserDTO);
            if (user.Succeeded) return Ok();
            else return BadRequest();
        }

        [HttpPost("refrash")]
        public async Task<IActionResult> Refresh(TokenDTO tokenDto)
        {
            var tokenDtoToReturn = await _serviceManager.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }
        [HttpGet("get-cliams")]
        public async Task<IActionResult> GetCliams(string userName)
        {
            var user = await _serviceManager.GetUserByUserName(userName);
            var claims = await _serviceManager.GetClaimsByUser(user);
            return Ok(claims);
        }



    }
}
