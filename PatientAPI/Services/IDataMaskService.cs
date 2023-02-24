namespace PatientAPI.Services
{
    public interface IDataMaskService
    {
        string Decrypt(string value);
        string Encrypt(string value);
    }
}