namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserByUserId(string userId);

        Task<string> GetUserNameByUserId(string userId);

        Task<User> CreateUser(UserForm userExtraInfoForm, string userId);

        Task UpdateUserByUserId(UserForm userExtraInfoForm, string userId);

        Task DeleteUserByUserId(string userId, string adminId);
    }
}
