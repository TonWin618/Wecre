using IdentityService.Domain;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.Controllers.Admin;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IdDomainService domainService;
    private readonly IdRepository repository;
    public AdminController(IdDomainService domainService,IdRepository repository)
    {
        this.repository = repository;
        this.domainService = domainService;
    }
    [HttpGet]
    public async Task<IQueryable<Role>> GetRoleList()
    {
        return await repository.GetRolesAsync();
    }
}

