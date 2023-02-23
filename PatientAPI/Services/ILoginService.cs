using PatientAPI.Models;

namespace PatientAPI.Services
{
    public interface ILoginService
    {
        string Login(Credential credential);
    }
}