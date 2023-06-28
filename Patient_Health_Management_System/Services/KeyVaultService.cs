namespace Patient_Health_Management_System.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly IConfiguration _configuration;
        public KeyVaultService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectingString() => _configuration["mongoDBConnectingString"];

        public Auth0KeyVault GetAuth0KeyVault()
        {
            return new Auth0KeyVault
            {
                Domain = _configuration["auth0Domain"],
                ClientId = _configuration["auth0ClientId"],
                ClientSecret = _configuration["auth0ClientSecret"],
                SigningCert = _configuration["auth0SigningCert"],
            };
        }
    }
}
