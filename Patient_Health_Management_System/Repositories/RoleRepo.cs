namespace Patient_Health_Management_System.Repositories
{
    public class RoleRepo : IRoleRepo
    {
        private readonly IMongoCollection<Role> _roles;

        public RoleRepo(MongoDbSetup mongoDbSetup)
        {
            _roles = mongoDbSetup.GetDatabase().GetCollection<Role>("roles");
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _roles.Find(role => true).ToListAsync();
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _roles.Find(role => role.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _roles.InsertOneAsync(role);
            return role;
        }

        public async Task ModifyRoleById(Role role)
        {
            await _roles.ReplaceOneAsync(r => r.Id == role.Id, role);
        }
    }
}
