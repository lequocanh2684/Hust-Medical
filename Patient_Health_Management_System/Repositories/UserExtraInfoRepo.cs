namespace Patient_Health_Management_System.Repositories
{
    public class UserExtraInfoRepo : IUserExtraInfoRepo
    {
        private readonly IMongoCollection<UserExtraInfo> _userExtraInfo;

        public UserExtraInfoRepo(MongoDbSetup mongoDbSetup)
        {
            _userExtraInfo = mongoDbSetup.GetDatabase().GetCollection<UserExtraInfo>("user_extra_info");
        }

        public async Task<UserExtraInfo> GetExtraInfoByUserId(string userId)
        {
            return await _userExtraInfo.Find(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<UserExtraInfo> CreateExtraInfo(UserExtraInfo userExtraInfo)
        {
            await _userExtraInfo.InsertOneAsync(userExtraInfo);
            return userExtraInfo;
        }

        public async Task ModifyExtraInfoById(UserExtraInfo userExtraInfo)
        {
            await _userExtraInfo.ReplaceOneAsync(u => u.Id == userExtraInfo.Id, userExtraInfo);
        }
    }
}
