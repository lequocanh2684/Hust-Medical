namespace Hust_Medical.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserByUserId(string userId);

        Task<string> GetUserNameByUserId(string userId);

        Task<User> CreateUser(UserForm userExtraInfoForm, string userId);

        Task UpdateUserByUserId(UserForm userExtraInfoForm, string userId, string adminId);

        Task DeleteUserByUserId(string userId, string adminId);

        Task ChangeUserEmailByUserId(string userId, string adminId, string newEmail);

        Task<long> GetNumberUsers();
    }
}
