using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientAPI.Dtos;
using PatientAPI.Services;

namespace PatientAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;
        private readonly IDataMaskService _dataMaskService;

        public PatientsController(ILogger<PatientsController> logger, IPatientService patientService, IDataMaskService dataMaskService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _dataMaskService = dataMaskService ?? throw new ArgumentNullException(nameof(dataMaskService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPatients()
        {
            var patientList = await _patientService.GetAllPatients();
            var patientDtoList = patientList?.Select(patient => MapFrom(patient));

            if (patientDtoList is null || !patientDtoList.Any())
            {
                _logger.LogError("Product list empty.");
                return NotFound();
            }
            _logger.LogInformation("Get Product list success.");
            return Ok(patientDtoList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatient(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var patient = MapFrom(await _patientService.GetById(_dataMaskService.Decrypt(id)));

            if (patient is null)
            {
                _logger.LogError("Product not found.");
                return NotFound();
            }

            _logger.LogInformation("Product found.");
            return Ok(patient);
        }

        private PatientDto MapFrom(Patient patient)
        {
            return new PatientDto
            {
                City = patient.City,
                AddressLine1 = patient.AddressLine1,

                PatientId = _dataMaskService.Encrypt(patient.PatientId),
                AddressLine2 = patient.AddressLine2,
                DateOfBirth = patient.DateOfBirth,
                FirstName = patient.FirstName,
                Gender = patient.Gender,
                LastName = patient.LastName,
                PostalCode = patient.PostalCode,
                State = patient.State,
            };
        }
    }
}
