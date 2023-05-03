namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();

        Task<Role> GetRoleById(string id);

        Task<Role> CreateRole(RoleForm roleForm, string userId);

        Task UpdateRoleById(string id, RoleForm roleForm, string userId);

        Task DeleteRoleById(string id, string userId);
    }
}
