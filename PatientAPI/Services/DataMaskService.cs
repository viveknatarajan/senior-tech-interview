using System.Web;
using Effortless.Net.Encryption;

namespace PatientAPI.Services
{
    public class DataMaskService : IDataMaskService
    {
        readonly byte[]  _key;
        readonly byte[]  _iv;

        public DataMaskService()
        {
            _key = Bytes.GenerateKey();
            _iv = Bytes.GenerateIV();
        }

        public string Decrypt(string value)
        {
            string cipherString = HttpUtility.UrlDecode(value);
            return Strings.Decrypt(cipherString, _key, _iv);
        }

        public string Encrypt(string value)
        {
            return HttpUtility.UrlEncode(Strings.Encrypt(value, _key, _iv));
        }
    }
}
