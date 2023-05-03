namespace Patient_Health_Management_System.Services
{
    public class UserExtraInfoService : IUserExtraInfoService
    {
        private readonly IUserExtraInfoRepo _userExtraInfoRepo;

        public UserExtraInfoService(IUserExtraInfoRepo userExtraInfoRepo)
        {
            _userExtraInfoRepo = userExtraInfoRepo;
        }

        public async Task<UserExtraInfo> GetExtraInfoByUserId(string userId)
        {
            try
            {
                return await _userExtraInfoRepo.GetExtraInfoByUserId(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserExtraInfo> CreateExtraInfo(UserExtraInfoForm userExtraInfoForm, string userId)
        {
            try
            {
                var uei = new UserExtraInfo()
                {
                    UserId = userId,
                    Address = userExtraInfoForm.Address,
                    PhoneNumber = userExtraInfoForm.PhoneNumber,
                    RoleId = userExtraInfoForm.RoleId,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt)
                };
                return await _userExtraInfoRepo.CreateExtraInfo(uei);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateExtraInfoByUserId(UserExtraInfoForm userExtraInfoForm, string userId)
        {
            try
            {
                var uei = await _userExtraInfoRepo.GetExtraInfoByUserId(userId);
                if (uei == null)
                {
                    throw new Exception("Extra info not found");
                }
                else
                {
                    uei.Address = userExtraInfoForm.Address;
                    uei.PhoneNumber = userExtraInfoForm.PhoneNumber;
                    uei.RoleId = userExtraInfoForm.RoleId;
                    uei.UpdatedBy = userId;
                    uei.UpdatedAt = DateTime.Now;
                    await _userExtraInfoRepo.ModifyExtraInfoById(uei);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteExtraInfoByUserId(string userId)
        {
            try
            {
                var uei = await _userExtraInfoRepo.GetExtraInfoByUserId(userId);
                if (uei == null)
                {
                    throw new Exception("Extra info not found");
                }
                else
                {
                    uei.IsDeleted = true;
                    uei.DeletedBy = userId;
                    uei.DeletedAt = DateTime.Now;
                    await _userExtraInfoRepo.ModifyExtraInfoById(uei);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
