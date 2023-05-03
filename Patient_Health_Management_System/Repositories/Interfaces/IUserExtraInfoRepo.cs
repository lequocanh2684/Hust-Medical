namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IUserExtraInfoRepo
    {
        Task<UserExtraInfo> GetExtraInfoByUserId(string userId);

        Task<UserExtraInfo> CreateExtraInfo(UserExtraInfo userExtraInfo);

        Task ModifyExtraInfoById(UserExtraInfo userExtraInfo);
    }
}
