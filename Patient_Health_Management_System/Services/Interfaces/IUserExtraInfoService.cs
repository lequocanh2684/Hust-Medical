namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IUserExtraInfoService
    {
        Task<UserExtraInfo> GetExtraInfoByUserId(string userId);

        Task<UserExtraInfo> CreateExtraInfo(UserExtraInfoForm userExtraInfoForm, string userId);

        Task UpdateExtraInfoByUserId(UserExtraInfoForm userExtraInfoForm, string userId);

        Task DeleteExtraInfoByUserId(string userId);
    }
}
