using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PatientAPI.Controllers;
using PatientAPI.Services;

namespace PatientAPI.UnitTests.Controllers
{
    public class PatientsControllerTests
    {
        readonly IPatientService patientService;
        readonly IDataMaskService dataMaskService;
        readonly PatientsController sut;


        public PatientsControllerTests()
        {
            patientService = Substitute.For<IPatientService>();
            dataMaskService = Substitute.For<IDataMaskService>();

            sut = new PatientsController(patientService, dataMaskService);
        }

        [Fact]
        public async Task GetAllPatients_ShouldReturnNotFound_ForEmptyApiResult()
        {
            patientService.GetAllPatients().Returns(Task.FromResult(Enumerable.Empty<Patient>()));

            var response = await sut.GetAllPatients() as NotFoundResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients_ForValidApiResult()
        {
            List<Patient> validPatients = new List<Patient> { new Patient { } };
            patientService.GetAllPatients().Returns(validPatients);

            var response = sut.GetAllPatients().Result as OkObjectResult;
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
        }
    }
}
