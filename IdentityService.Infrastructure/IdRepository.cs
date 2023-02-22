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

        public Task<IdentityResult> AccessFailedAsync(User user)
        {
            return userManager.AccessFailedAsync(user);
        }

        public async Task<bool> RoleExistsAsync(User user, string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRoleAsync(Role role)
        {
            return await roleManager.CreateAsync(role);
        }
        public async Task<IQueryable<Role>> GetRolesAsync()
        {
            return roleManager.Roles;
        }
        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<IdentityResult> RemoveFromRoleAsync(User user,string roleName)
        {
            return await userManager.RemoveFromRoleAsync(user, roleName);
        }
        public async Task<bool> IsLockedOutAsync(User user)
        {
            return await userManager.IsLockedOutAsync(user);
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<User?> FindByNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }
        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }
        public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail,string token)
        {
            return await userManager.ChangeEmailAsync(user, newEmail, token);
        }
        public async Task<IdentityResult> ChangePasswordAsync(User user, string curPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, curPassword, newPassword);
        }
        public async Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
        {
            return await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }
    }
}
