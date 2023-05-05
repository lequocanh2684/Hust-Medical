﻿namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByUserId(string userId);

        Task<User> CreateUser(UserForm userExtraInfoForm, string userId);

        Task UpdateUserByUserId(UserForm userExtraInfoForm, string userId);

        Task DeleteUserByUserId(string userId);
    }
}
