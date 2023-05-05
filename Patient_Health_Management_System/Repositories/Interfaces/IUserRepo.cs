namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetUserByUserId(string userId);

        Task<User> CreateUser(User user);

        Task ModifyUserByUserId(User user);
    }
}
