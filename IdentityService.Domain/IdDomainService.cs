using Common.JWT;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace IdentityService.Domain;

public class IdDomainService
{
    private readonly IIdRepository repository;
    private readonly ITokenService tokenService;
    private readonly IOptions<JWTOptions> optJWT;
    private readonly IEmailSender emailSender;

    public IdDomainService(IIdRepository repository, ITokenService tokenService, IOptions<JWTOptions> optJWT)
    {
        this.repository = repository;
        this.tokenService = tokenService;
        this.optJWT = optJWT;
    }
    public async Task<IdentityResult> SignUp(string userName,string email,string password)
    {
        User user = new(userName);
        user.Email = email;
        var result = await repository.CreateUserAsync(user, password);
        return result;
    }
    public async Task<(SignInResult, string)> LoginByUserNameAndPwdAsync(string userName, string password)
    {
        var user = await repository.FindByNameAsync(userName);
        return await CheckPasswordAsync(user, password);
    }

    public async Task<(SignInResult, string)> LoginByEmailAndPwdAsync(string email, string password)
    {
        var user = await repository.FindByEmailAsync(email);
        return await CheckPasswordAsync(user, password);
    }
    public async Task<IdentityResult> AddUserToRoleAsync(User user, string roleName)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        if (await repository.RoleExistsAsync(user, roleName))
        {
            return IdentityResult.Failed();
        }
        var result = await repository.AddToRoleAsync(user, roleName);
        return result;
    }
    public async Task<bool> SendEmailTokenAsync(User user, string newEmail)
    {
        string token = await repository.GenerateChangeEmailTokenAsync(user, newEmail);
        emailSender.SendChangeTokenAsync(newEmail, token);
        return true;
    }
    public async Task<bool> SendEmailChangeLinkAsync(User user, string newEmail)
    {
        string token = await repository.GenerateChangeEmailTokenAsync(user, newEmail);
        emailSender.SendChangeTokenAsync(newEmail, token);
        return true;
    }
    private async Task<string> BuildTokenAsync(User user)
    {
        var roles = await repository.GetRolesAsync(user);
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
        foreach(string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return tokenService.BuildToken(claims, optJWT.Value);
    }
    private async Task<(SignInResult,string)> CheckPasswordAsync(User user, string password)
    {
        if (user == null)
        {
            return (SignInResult.LockedOut, null);
        }
        if (await repository.IsLockedOutAsync(user))
        {
            return (SignInResult.Failed, null);
        }
        if (await repository.CheckPasswordAsync(user, password))
        {
            return (SignInResult.Success, await BuildTokenAsync(user!));
        }
        return (SignInResult.Failed, await BuildTokenAsync(user!));
    }
}