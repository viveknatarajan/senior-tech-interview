using PatientAPI.ConfigurationSections;
using System.Text;

namespace PatientAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _patientApiUri;

        public PatientService(HttpClient httpClient, ApiConfiguration apiConfiguration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _patientApiUri = apiConfiguration?.BaseUrl
                             ?? throw new ArgumentNullException(nameof(apiConfiguration.BaseUrl)); ;
        }

        public async Task<Patient?> GetById(string id)
        {
            Patient? patient = null;

            var response = await _httpClient.GetAsync($"{_patientApiUri}/patient/{id}");
            if (response.IsSuccessStatusCode)
            {
                patient = await response.Content.ReadFromJsonAsync<Patient>();
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            IEnumerable<Patient>? patients = Enumerable.Empty<Patient>();
            var response = await _httpClient.GetAsync($"{_patientApiUri}/patients");
            if (response.IsSuccessStatusCode)
            {
                patients = await response.Content.ReadFromJsonAsync<IEnumerable<Patient>>() ?? Enumerable.Empty<Patient>();
            }
            return patients;
        }
    }
}
