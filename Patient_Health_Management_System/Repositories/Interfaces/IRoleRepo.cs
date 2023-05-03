namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IRoleRepo
    {
        Task<IEnumerable<Role>> GetRoles();

        Task<Role> GetRoleById(string id);

        Task<Role> CreateRole(Role role);

        Task ModifyRoleById(Role role);
    }
}
