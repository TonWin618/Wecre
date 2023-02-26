using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Infrastructure
{
    public class IdRepository : IIdRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public IdRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> FindByNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IdentityResult> AccessFailedAsync(User user)
        {
            return userManager.AccessFailedAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IdentityResult> ResetAccessFailedCountAsync(User user)
        {
            return userManager.ResetAccessFailedCountAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> IsLockedOutAsync(User user)
        {
            return await userManager.IsLockedOutAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> RoleExistsAsync(User user, string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRoleAsync(Role role)
        {
            return await roleManager.CreateAsync(role);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveFromRoleAsync(User user,string roleName)
        {
            return await userManager.RemoveFromRoleAsync(user, roleName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GenerateEmailTokenAsync(User user)
        {
            return await userManager.GenerateTwoFactorTokenAsync(user, "Email");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> VerifyEmailTokenAsync(User user, string token)
        {
            return await userManager.VerifyTwoFactorTokenAsync(user, "Email",token);    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdatePassword(User user, string newPassword)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateEmail(User user, string newEmail)
        {
            var token = await userManager.GenerateChangeEmailTokenAsync(user,newEmail);
            return await userManager.ChangeEmailAsync(user, newEmail, token);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newUserName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUserName(User user, string newUserName)
        {
            return await userManager.SetUserNameAsync(user, newUserName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateConfirmEmailTokenAsync(User user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IdentityResult> VerifyConfirmEmailTokenAsync(User user,string token)
        {
            return await userManager.ConfirmEmailAsync(user, token);
        }
    }
}
