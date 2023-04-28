using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using RestSharp;

namespace Patient_Health_Management_System.Services
{
    public class AccountService : IAccountService
    {
        public async Task<string> TokenGenerator()
        {
            string directory = Directory.GetCurrentDirectory();
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();
            var domain = configurationRoot.GetSection("Auth0").GetSection("Domain").Value;
            var client_id = configurationRoot.GetSection("Auth0").GetSection("ClientId").Value;
            var client_secret = configurationRoot.GetSection("Auth0").GetSection("ClientSecret").Value;

            var client = new RestClient($"https://{domain}/oauth/token");
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody($"{{\"client_id\":\"{client_id}\",\"client_secret\":\"{client_secret}\",\"audience\":\"https://{domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}");
            var response = await client.PostAsync<Auth0Token>(request);
            return response.access_token;
        }
    }
}