namespace Patient_Health_Management_System.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepo _userRepo;

		public UserService(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		public async Task<IEnumerable<User>> GetUsers()
		{
			try
			{
				return await _userRepo.GetUsers();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public async Task<User> GetUserByUserId(string userId)
		{
			try
			{
				return await _userRepo.GetUserByUserId(userId);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public async Task<string> GetUserNameByUserId(string userId)
		{
			try
			{
				return await _userRepo.GetUserNameByUserId(userId);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public async Task<User> CreateUser(UserForm userForm, string adminId)
		{
			try
			{
				var uei = new User()
				{
					UserId = userForm.UserId,
					Name = userForm.Name,
					Email = userForm.Email,
					Address = userForm.Address,
					PhoneNumber = userForm.PhoneNumber,
					Specialist = userForm.Specialist,
					Gender = userForm.Gender,
					Role = userForm.Role,
					CreatedBy = adminId,
					CreatedAt = DateTime.Now,
					UpdatedBy = null,
					UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
					IsDeleted = false,
					DeletedBy = null,
					DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt)
				};
				return await _userRepo.CreateUser(uei);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task UpdateUserByUserId(UserForm userForm, string userId, string adminId)
		{
			try
			{
				var uei = await _userRepo.GetUserByUserId(userId);
				if (uei == null)
				{
					throw new Exception("user not found");
				}
				else
				{
					uei.Name = userForm.Name;
					uei.Email = userForm.Email;
					uei.Address = userForm.Address;
					uei.PhoneNumber = userForm.PhoneNumber;
					uei.Specialist = userForm.Specialist;
					uei.Gender = userForm.Gender;
					uei.Role = userForm.Role;
					if (adminId.Equals(""))
					{
						uei.UpdatedBy = userId;
					}
					else uei.UpdatedBy = adminId;
                    uei.UpdatedAt = DateTime.Now;
					await _userRepo.ModifyUserByUserId(uei);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task DeleteUserByUserId(string userId, string adminId)
		{
			try
			{
				var uei = await _userRepo.GetUserByUserId(userId);
				if (uei == null)
				{
					throw new Exception("user not found");
				}
				else
				{
					uei.IsDeleted = true;
					uei.DeletedBy = adminId;
					uei.DeletedAt = DateTime.Now;
					await _userRepo.ModifyUserByUserId(uei);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task ChangeUserEmailByUserId(string userId, string adminId, string newEmail)
		{
            try
			{
                var uei = await _userRepo.GetUserByUserId(userId);
                if (uei == null)
				{
                    throw new Exception("user not found");
                }
                else
				{
                    uei.Email = newEmail;
                    uei.UpdatedBy = adminId;
                    uei.UpdatedAt = DateTime.Now;
                    await _userRepo.ModifyUserByUserId(uei);
                }
            }
            catch (Exception ex)
			{
                throw new Exception(ex.Message);
            }
        }
	}
}
