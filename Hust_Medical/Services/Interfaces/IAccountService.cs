using RestSharp;

namespace Hust_Medical.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Auth0Token> TokenGenerator();

        Task<UserResponse> GetUserById(string id, string access_token);

        Task<IEnumerable<UserResponse>> GetUserByEmail(string accessToken, string email);

        Task<IEnumerable<Role>> GetRoles(string access_token);

        Task AssignRolesToUserByUserId(string id, string access_token, IEnumerable<string> roleIds);

        Task<RestResponse> CreateUser(string access_token, AccountForm accountForm);

        Task UpdateUserById(string id, string access_token, AccountForm accountForm);

        Task BlockUserById(string id, string access_token);

        Task SendVerificationEmail(string id, string access_token);

        Task ChangePassword(string id, string access_token, string password);

        Task ChangeEmail(string id, string accessToken, string email);

        Task UpdateUserNameById(string id, string access_token, string userName);

        Task RemoveRolesFromUserByUserId(string id, string access_token, IEnumerable<string> roleIds);

        bool IsExpired(string access_token);
    }
}