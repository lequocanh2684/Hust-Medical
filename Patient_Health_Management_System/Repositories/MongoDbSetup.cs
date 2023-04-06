namespace Patient_Health_Management_System.Repositories
{
    public class MongoDbSetup
    {
        private readonly IConfiguration _config;

        public MongoDbSetup(IConfiguration config)
        {
            _config = config;
        }

        public IMongoDatabase GetDatabase()
        {   string directory = Directory.GetCurrentDirectory();
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();
            var client = new MongoClient(configurationRoot.GetConnectionString("MongoDBConnection"));
            return client.GetDatabase("patient_health_db");
        }
    }
}
