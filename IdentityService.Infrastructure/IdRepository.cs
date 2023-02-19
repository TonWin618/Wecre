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

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            if(!await roleManager.RoleExistsAsync(roleName))
            {
                Role role = new Role { Name=roleName};
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return result;
                }
            }
            return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<SignInResult> CheckPwdAsync(User user, string password)
        {
            if(await userManager.IsLockedOutAsync(user))
            {
                return SignInResult.LockedOut;
            }
            var result = await userManager.CheckPasswordAsync(user, password);
            if (result == true)
            {
                return SignInResult.Success;
            }
            else
            {
                await AccessFailedAsync(user);
                return SignInResult.Failed;
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
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
    }
}
