using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain;

public interface IIdRepository
{
    Task<IdentityResult> CreateUserAsync(User user,string password);//Create a User
    Task<User?> FindByNameAsync(string userName);//Get a user by email address
    Task<User?> FindByEmailAsync(string email);//Get a user by username
    Task<bool> CheckPasswordAsync(User user, string password);//Check password
    Task<bool> IsLockedOutAsync(User user);//
    Task<IdentityResult> AccessFailedAsync(User user); //Log a failed login
    Task<IList<string>> GetRolesAsync(User user);//Get all roles for user

    Task<string> GenerateConfirmEmailTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<string> GenerateTFTokenAsync(User user, string tokenProvider);
    Task<bool> VerifyTFTokenAsync(User user, string tokenProvider, string token);
    Task<string> GenerateEmailTokenAsync(User user);
    Task<string> VerifyEmailTokenAsync(User user, string token);

    Task<IdentityResult> UpdatePassword(User user, string newPassword);
    Task<IdentityResult> UpdateEmail(User user, string newEmail);
    Task<IdentityResult> UpdateUserName(User user, string newUserName);

    Task<bool> RoleExistsAsync(User user, string roleName);
    IQueryable<Role> GetRoleList();
    Task<IdentityResult> CreateRoleAsync(Role role);
    Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
}
