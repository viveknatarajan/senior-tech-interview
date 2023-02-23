using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace PatientAPI
{
    public class Patient
    {
        public int Id => GetHashCode();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() * 17 + this.LastName.GetHashCode() ;
        }

    }
}