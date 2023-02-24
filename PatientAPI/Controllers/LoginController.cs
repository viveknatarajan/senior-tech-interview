using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientAPI.Models;
using PatientAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(Credential credential)
        {
            if (credential == null || string.IsNullOrWhiteSpace(credential.Email) || string.IsNullOrWhiteSpace(credential.Password))
            {
                _logger.LogError("Bad credentials.");
                return BadRequest();
            }
            string token = _loginService.Login(credential);

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogError($"Failed to login {credential.Email}");
                return Unauthorized();
            }
            else
            {
                _logger.LogInformation("Login success");
                return Ok(token);
            }
        }
    }
}
