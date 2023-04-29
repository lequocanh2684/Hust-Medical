using RestSharp;

namespace Patient_Health_Management_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly string domain;
        private readonly string client_id;
        private readonly string client_secret;

        public AccountService(IConfiguration configuration)
        {
            domain = configuration.GetSection("Auth0").GetSection("Domain").Value;
            client_id = configuration.GetSection("Auth0").GetSection("ClientId").Value;
            client_secret = configuration.GetSection("Auth0").GetSection("ClientSecret").Value;
        }

        public async Task<string> TokenGenerator()
        {
            var client = new RestClient($"https://{domain}/oauth/token");
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody($"{{\"client_id\":\"{client_id}\",\"client_secret\":\"{client_secret}\",\"audience\":\"https://{domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}");
            var response = await client.PostAsync<Auth0Token>(request);
            return response.access_token;
        }

        public async Task<UserResponse> GetUserResponseById(string id, string access_token)
        {
            var client = new RestClient($"https://{domain}/api/v2/users/{id}");
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {access_token}");
            var response = await client.GetAsync<UserResponse>(request);
            return response;
        }
    }
}