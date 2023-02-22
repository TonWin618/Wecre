using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain;

public interface IIdRepository
{
    Task<IdentityResult> CreateUserAsync(User user,string password);//Create a User
    Task<User?> FindByNameAsync(string userName);//Get a user by email address
    Task<User?> FindByEmailAsync(string email);//Get a user by username
    Task<bool> IsLockedOutAsync(User user);
    Task<bool> CheckPasswordAsync(User user, string password);//Check password
    Task<IdentityResult> AccessFailedAsync(User user); //Log a failed login
    Task<IList<string>> GetRolesAsync(User user);//Get all roles for user

    Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
    Task<string> CreateEmailTokenAsync(User user);
    Task<IdentityResult> ChangePasswordAsync(User user, string curPassword, string newPassword);
    Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);

    Task<bool> RoleExistsAsync(User user, string roleName);
    Task<IQueryable<Role>> GetRolesAsync();
    Task<IdentityResult> CreateRoleAsync(Role role);
    Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
}
