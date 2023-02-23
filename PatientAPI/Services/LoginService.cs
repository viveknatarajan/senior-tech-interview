using Microsoft.IdentityModel.Tokens;
using PatientAPI.ConfigurationSections;
using PatientAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public LoginService(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public string Login(Credential credential)
        {
            string accessToken = string.Empty;
            if (credential.Email == "test@test.com" && credential.Password == "test")
            {
                var issuer = _jwtConfiguration.Issuer;
                var audience = _jwtConfiguration.Audience;
                var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Email, credential.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                accessToken = tokenHandler.WriteToken(token);
            }
            return accessToken;
        }
    }
}
