namespace PatientAPI.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient?> GetById(int id);
    }
}