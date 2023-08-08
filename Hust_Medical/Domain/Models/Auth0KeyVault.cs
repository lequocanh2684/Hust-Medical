namespace Hust_Medical.Domain.Models
{
    public class Auth0KeyVault
    {
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SigningCert { get; set; }
    }
}
