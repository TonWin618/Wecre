using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain;

public interface IIdentityRepository
{
    Task<IdentityResult> CreateAsync(User user,string password);//Create a User
    Task<User?> FindByNameAsync(string userName);//Get a user by email address
    Task<User?> FindByEmailAsync(string email);//Get a user by username
    Task<SignInResult> CheckPwdAsync(User user, string password);//Check password
    Task<IdentityResult> AccessFailedAsync(User user); //Log a failed login
    Task<IList<string>> GetRolesAsync(User user);//Get all roles for user
    Task<IdentityResult> AddToRoleAsync(User user, string role);//add user to that role
}
