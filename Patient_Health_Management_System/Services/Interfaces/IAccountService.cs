namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Auth0Token> TokenGenerator();

        Task<UserResponse> GetUserResponseById(string id, string access_token);

        Task UpdateUserById(string id, string access_token, AccountForm accountForm);

        Task BlockUserById(string id, string access_token);

        Task SendVerificationEmail(string id, string access_token);

        bool IsExpired(string access_token);
    }
}