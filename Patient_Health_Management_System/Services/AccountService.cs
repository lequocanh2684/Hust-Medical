using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Patient_Health_Management_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly string domain;
        private readonly string client_id;
        private readonly string client_secret;

        private IJsonSerializer _serializer;
        private IDateTimeProvider _provider;
        private IBase64UrlEncoder _urlEncoder;

        private readonly string _signingCert;

        public AccountService(IConfiguration configuration)
        {
            domain = configuration.GetSection("Auth0").GetSection("Domain").Value;
            client_id = configuration.GetSection("Auth0").GetSection("ClientId").Value;
            client_secret = configuration.GetSection("Auth0").GetSection("ClientSecret").Value;
            _signingCert = configuration.GetSection("Auth0").GetSection("SigningCert").Value;
            _serializer = new JsonNetSerializer();
            _provider = new UtcDateTimeProvider();
            _urlEncoder = new JwtBase64UrlEncoder();
        }

        public async Task<Auth0Token> TokenGenerator()
        {
            var client = new RestClient($"https://{domain}/oauth/token");
            var request = new RestRequest();
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody($"{{\"client_id\":\"{client_id}\",\"client_secret\":\"{client_secret}\",\"audience\":\"https://{domain}/api/v2/\",\"grant_type\":\"client_credentials\"}}");
            var response = await client.PostAsync<Auth0Token>(request);
            return response;
        }

        public async Task<UserResponse> GetUserById(string id, string access_token)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                var response = await client.GetAsync<UserResponse>(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserResponse>> GetUserByEmail(string accessToken, string email)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users-by-email");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {accessToken}");
                request.AddQueryParameter("email", email);
                var response = await client.GetAsync(request);
                var users = JsonSerializer.Deserialize<IEnumerable<UserResponse>>(response.Content);
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Role>> GetRoles(string access_token)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/roles");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                var response = await client.GetAsync(request);
                var roles = JsonSerializer.Deserialize<IEnumerable<Role>>(response.Content);
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateUser(string access_token, AccountForm accountForm)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                var body = JsonSerializer.Serialize(accountForm);
                request.AddJsonBody(body);
                await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task UpdateUserById(string id, string access_token, AccountForm accountForm)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody(accountForm.ToJson().Except("password"));
                await client.PatchAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task BlockUserById(string id, string access_token)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"blocked\":true}}");
                await client.PatchAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AssignRolesToUserByUserId(string id, string access_token, IEnumerable<string> roleIds)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}/roles");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"roles\":{JsonSerializer.Serialize(roleIds)}}}");
                await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SendVerificationEmail(string id, string access_token)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/jobs/verification-email");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"user_id\":\"{id}\",\"client_id\":\"{client_id}\"}}");
                await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsExpired(string access_token)
        {
            try
            {
                IJwtAlgorithm _algorithm = new RS256Algorithm(CreateCertificate());
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var _token = decoder.DecodeToObject<Auth0Token>(access_token);
                return false;
            }
            catch (TokenExpiredException tee)
            {
                return true;
            }
            catch (SignatureVerificationException sve)
            {
                throw new SignatureVerificationException(sve.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private X509Certificate2 CreateCertificate()
        {
            return new X509Certificate2(Convert.FromBase64String(_signingCert));
        }

        public async Task ChangePassword(string id, string access_token, string password)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"password\":\"{password}\",\"connection\":\"Username-Password-Authentication\"}}");
                await client.PatchAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ChangeEmail(string id, string accessToken, string email)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {accessToken}");
                request.AddJsonBody($"{{\"email\":\"{email}\",\"connection\":\"Username-Password-Authentication\"}}");
                await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserNameById(string id, string access_token, string userName)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"name\":\"{userName}\"}}");
                await client.PatchAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveRolesFromUserByUserId(string id, string access_token, IEnumerable<string> roleIds)
        {
            try
            {
                var client = new RestClient($"https://{domain}/api/v2/users/{id}/roles");
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", $"Bearer {access_token}");
                request.AddJsonBody($"{{\"roles\":{JsonSerializer.Serialize(roleIds)}}}");
                await client.DeleteAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}