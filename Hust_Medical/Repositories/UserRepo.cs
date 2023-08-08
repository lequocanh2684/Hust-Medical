using Patient_Health_Management_System.Domain.Models;

namespace Patient_Health_Management_System.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IMongoCollection<User> _user;

        public UserRepo(RepoInitialize mongoDbSetup)
        {
            _user = mongoDbSetup.GetDatabase().GetCollection<User>("user_extra_info");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _user.Find(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            return await _user.Find(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<string> GetUserNameByUserId(string userId)
        {
            return await _user.Find(u => u.UserId == userId).Project(u => u.Name).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User User)
        {
            await _user.InsertOneAsync(User);
            return User;
        }

        public async Task ModifyUserByUserId(User User)
        {
            await _user.ReplaceOneAsync(u => u.Id == User.Id, User);
        }
        public async Task<long> GetNumberUsers()
        {
            try
            {
                return await _user.Find(u=> !u.IsDeleted).CountDocumentsAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
