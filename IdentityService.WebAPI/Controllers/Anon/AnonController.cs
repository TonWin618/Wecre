using IdentityService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace IdentityService.WebAPI.Controllers.Anon;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AnonController : ControllerBase
{
    private readonly IIdentityRepository repository;
    private readonly IdentityDomainService domainService;

    public AnonController(IIdentityRepository repository, IdentityDomainService domainService)
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
        if(IdentityResult.Success == await domainService.SignUp(req.UserName, req.Email, req.Password)){
            return Ok("Registered successfully");
        }
        else
        {
            return BadRequest("Registration failed");
        }
        
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
            return StatusCode((int)HttpStatusCode.Locked, "The user is locked");
        }
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("Login failure. " + msg);
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginByUserNameAndPwd(LoginByUserNameAndPwdRequest req)
    {
        (var checkResult, var token) = await domainService.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }
        else if (checkResult.IsLockedOut)
        {
            return StatusCode((int)HttpStatusCode.Locked, "The user is locked");
        }
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("Login failure. " + msg);
        }
    }
}
