using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Domain;
using IdentityService.Infrastructure;
using IdentityService.Domain.Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityService.WebAPI.Controllers.Admin;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IdDomainService domainService;
    private readonly IdRepository repository;
    public AdminController(IdDomainService domainService,IdRepository repository)
    {
        this.repository = repository;
        this.domainService = domainService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateWorld()
    {
        if (repository.FindByNameAsync("admin") == null)
        {
            return BadRequest("User(admin) Exists");
        }
        var user = new User("admin");
        repository.AddToRoleAsync(user, "admin");
        await repository.CreateAsync(user,"tonwin");
        return Ok(" ");
    }
    [HttpPost]
    public async Task<ActionResult<string>> AddAdminUser(AddAdminUserRequest req)
    {
        
    }
    [HttpPost]
    public async Task<ActionResult<string>> RemoveAdminUser()
    {

    }
    [HttpGet]
    public async Task<ActionResult<string>> UserInfoList()
    {

    }
}

