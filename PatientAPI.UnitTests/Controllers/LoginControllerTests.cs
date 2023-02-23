using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PatientAPI.Controllers;
using PatientAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PatientAPI.UnitTests.Controllers
{
    public class LoginControllerTests
    {
        LoginController sut;
        ILoginService mockLoginService;

        public LoginControllerTests()
        {
            mockLoginService = Substitute.For<ILoginService>();
            sut = new LoginController(mockLoginService);
        }

        [Fact]
        public void Login_ShouldReturnBadRequest_ForEmptyCredential()
        {
            //var response = sut.Login(null) as BadRequestObjectResult;
            //response.Should().NotBeNull();
            //response.StatusCode.Should().Be(400);
        }
    }
}
