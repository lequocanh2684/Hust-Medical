namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Auth0Token> TokenGenerator();

        Task<UserResponse> GetUserResponseById(string id, string access_token);
    }
}