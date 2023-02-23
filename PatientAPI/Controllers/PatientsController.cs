using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientAPI.Services;

namespace PatientAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await patientService.GetAllPatients();

            if (patients is null || !patients.Any())
            {
                return NotFound();
            }

            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await patientService.GetById(id);

            if (patient is null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
