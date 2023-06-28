namespace Patient_Health_Management_System.Domain.Models
{
    public class Auth0KeyVault
    {
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SigningCert { get; set; }
    }
}
