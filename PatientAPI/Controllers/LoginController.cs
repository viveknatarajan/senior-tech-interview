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
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
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
                return BadRequest();
            }
            string token = _loginService.Login(credential);
            return string.IsNullOrWhiteSpace(token) ? Unauthorized() : Ok(token);
        }
    }
}
