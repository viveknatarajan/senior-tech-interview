using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PatientAPI.Controllers;
using PatientAPI.Services;

namespace PatientAPI.UnitTests.Controllers
{
    public class LoginControllerTests
    {
        readonly LoginController _sut;
        readonly ILoginService _loginService;
        readonly ILogger<LoginController> _logger;
        public LoginControllerTests()
        {
            _loginService = Substitute.For<ILoginService>();
            _logger = Substitute.For<ILogger<LoginController>>();
            _sut = new LoginController(_logger, _loginService);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("test@test.com", null)]
        [InlineData(null, "fake-password")]
        public void Login_ShouldReturnBadResult_ForNullCredentials(string email, string password)
        {
            var response = _sut.Login(new Models.Credential { Email = email, Password = password }) as BadRequestResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Login_ShouldReturnUnAuthorizedResult_ForEmptyToken()
        {
            Models.Credential credential = new Models.Credential { Email = "test@test.com", Password = "test" };
            _loginService.Login(credential).Returns(string.Empty);

            var response = _sut.Login(credential) as UnauthorizedResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(401);
        }

        [Fact]
        public void Login_ShouldReturnOKResult_ForValidToken()
        {
            Models.Credential credential = new Models.Credential { Email = "test@test.com", Password = "test" };
            _loginService.Login(credential).Returns("fake-token");

            var response = _sut.Login(credential) as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().Be("fake-token");
        }

    }
}
