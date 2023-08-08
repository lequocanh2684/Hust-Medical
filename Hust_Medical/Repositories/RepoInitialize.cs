namespace Patient_Health_Management_System.Repositories
{
    public class RepoInitialize
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly IMongoClient _client;

        public RepoInitialize(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
            _client = new MongoClient(_keyVaultService.GetConnectingString());
        }

        public IMongoDatabase GetDatabase()
        {
            return _client.GetDatabase("patient_health_db");
        }
    }
}
