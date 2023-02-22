using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.Controllers.Admin;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class AdminController : ControllerBase
{
    private readonly IIdRepository repository;
    private readonly IdDomainService domainService;
    public AdminController(IdDomainService domainService,IIdRepository repository)
    {
        this.repository = repository;
        this.domainService = domainService;
    }
    [HttpPost]
    public async Task<ActionResult> CreateRole(CreateRoleRequest req)
    {
        Role role = new() { Name = req.roleName };
        if (IdentityResult.Success == await repository.CreateRoleAsync(role))
        {
            return Ok("The role is created successfully");
        };
        return BadRequest();
    }
}

