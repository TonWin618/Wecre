using Common.ASPNETCore;
using IdentityService.Domain;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;

namespace IdentityService.WebAPI.Controllers.Anon;

[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
[UnitOfWork(typeof(IdDbContext))]
public class AnonController : ControllerBase
{
    private readonly IIdRepository repository;
    private readonly IdDomainService domainService;

    public AnonController(IIdRepository repository, IdDomainService domainService)
    {
        this.repository = repository;
        this.domainService = domainService;
    }
    [HttpPost]
    public async Task<ActionResult<string>> SignUp(SignUpRequest req)
    {
        if (req.UserName.IsNullOrEmpty() || req.UserName.Length < 6)
        {
            return BadRequest("Format error");
        }
        if(req.Email.IsNullOrEmpty())
        {
            return BadRequest("Format error");
        }
        if (req.Password.IsNullOrEmpty() || req.Password.Length < 6) 
        {
            return BadRequest("Format error");
        }
        if (null != await repository.FindByNameAsync(req.UserName))
        {
            return BadRequest("The user name you have selected is already taken");
        }
        if(IdentityResult.Success == await domainService.SignUp(req.UserName, req.Email, req.Password)){
            return Ok("Registered successfully");
        }
        return BadRequest("Registration failed");
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginByEmailAndPwd(LoginByEmailAndPwdRequest req)
    {
        (var checkResult,var token) = await domainService.LoginByEmailAndPwdAsync(req.EmailAddr, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }
        else if (checkResult.IsLockedOut)
        {
            return StatusCode((int)HttpStatusCode.Locked, "The user is locked out");
        }
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("Login failure" + msg);
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginByUserNameAndPwd(LoginByUserNameAndPwdRequest req)
    {
        (var checkResult, var token) = await domainService.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
        if (checkResult.Succeeded)
        {
            //integration event test code
            var user  = await repository.FindByNameAsync(req.UserName);
            user.TestEvent();
            //integration event test code
            return token;
        }
        else if (checkResult.IsLockedOut)
        {
            return StatusCode((int)HttpStatusCode.Locked, "The user is locked out");
        }
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("Login failure. " + msg);
        }
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateWorld()
    {
        if (repository.FindByNameAsync("admin") == null)
        {
            return BadRequest();
        }

        var user = new Domain.Entities.User("admin");
        user.Email= "admin@admin.com";
        if (IdentityResult.Success != await repository.CreateUserAsync(user, "123456"))
        {
            return BadRequest();
        };

        Role role = new Role { Name = "Admin" };
        if (IdentityResult.Success != await repository.CreateRoleAsync(role))
        {
            return BadRequest();
        }

        if (IdentityResult.Success != await repository.AddToRoleAsync(user, "Admin"))
        {
            return BadRequest();
        };
        return Ok();
    }
}
