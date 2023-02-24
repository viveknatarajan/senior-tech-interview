using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PatientAPI.Controllers;
using PatientAPI.Services;

namespace PatientAPI.UnitTests.Controllers
{
    public class PatientsControllerTests
    {
        readonly IPatientService _mockPatientService;
        readonly IDataMaskService _mockDataMaskService;
        readonly ILogger<PatientsController> _logger;
        readonly PatientsController _sut;


        public PatientsControllerTests()
        {
            _mockPatientService = Substitute.For<IPatientService>();
            _mockDataMaskService = Substitute.For<IDataMaskService>();
            _logger = Substitute.For<ILogger<PatientsController>>();

            _sut = new PatientsController(_logger, _mockPatientService, _mockDataMaskService);
        }

        [Fact]
        public async Task GetAllPatients_ShouldReturnNotFound_ForEmptyApiResult()
        {
            _mockPatientService.GetAllPatients().Returns(Task.FromResult(Enumerable.Empty<Patient>()));

            var response = await _sut.GetAllPatients() as NotFoundResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients_ForValidApiResult()
        {
            List<Patient> validPatients = new List<Patient> { new Patient { } };
            _mockPatientService.GetAllPatients().Returns(validPatients);

            var response = _sut.GetAllPatients().Result as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }
    }
}
