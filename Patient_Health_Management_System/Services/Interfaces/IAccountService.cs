namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Auth0Token> TokenGenerator();

        Task<UserResponse> GetUserById(string id, string access_token);

        Task<IEnumerable<UserResponse>> GetUserByEmail(string accessToken, string email);

		Task<IEnumerable<Role>> GetRoles(string access_token);

        Task AssignRolesToUserByUserId(string id, string access_token, IEnumerable<string> roleIds);

        Task CreateUser(string access_token, AccountForm accountForm);

        Task UpdateUserById(string id, string access_token, AccountForm accountForm);

        Task BlockUserById(string id, string access_token);

        Task SendVerificationEmail(string id, string access_token);

        bool IsExpired(string access_token);
    }
}