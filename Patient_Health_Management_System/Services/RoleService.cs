namespace Patient_Health_Management_System.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                return await _roleRepo.GetRoles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> GetRoleById(string id)
        {
            try
            {
                return await _roleRepo.GetRoleById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> CreateRole(RoleForm roleForm, string userId)
        {
            try
            {
                var role = new Role()
                {
                    RoleName = roleForm.RoleName,
                    RolePermission = roleForm.RolePermission,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt)
                };
                return await _roleRepo.CreateRole(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRoleById(string id, RoleForm roleForm, string userId)
        {
            try
            {
                var role = await _roleRepo.GetRoleById(id);
                if (role == null)
                {
                    throw new Exception("Role not found");
                }
                else
                {
                    role.RoleName = roleForm.RoleName;
                    role.RolePermission = roleForm.RolePermission;
                    role.UpdatedAt = DateTime.Now;
                    role.UpdatedBy = userId;
                    await _roleRepo.ModifyRoleById(role);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteRoleById(string id, string userId)
        {
            try
            {
                var role = await _roleRepo.GetRoleById(id);
                if (role == null)
                {
                    throw new Exception("Role not found");
                }
                else
                {
                    role.IsDeleted = true;
                    role.DeletedAt = DateTime.Now;
                    role.DeletedBy = userId;
                    await _roleRepo.ModifyRoleById(role);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
