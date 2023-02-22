using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.WebAPI.Controllers.User
{
    [Route("api/[controller]")]
    [Authorize(two)]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IIdRepository repository;
        private readonly IdDomainService domainService;
        public UserController(IIdRepository repository, IdDomainService domainService)
        {
            this.repository = repository;
            this.domainService=domainService;
        }
        [HttpPost]
        public async Task<ActionResult> ChangeMyPassword(ChangeMyPasswordRequest req)
        {
            string userName = this.User.FindFirstValue(ClaimTypes.Name);
            Domain.Entities.User user = await repository.FindByNameAsync(userName);
            if(IdentityResult.Success == await repository.ChangePasswordAsync(user, req.curPassword, req.newPassword))
            {
                return Ok("Your password is changed");
            };
            return BadRequest("Password change failed");
        }
        [HttpPost]
        public async Task<ActionResult> ChangeMyEmail(ChangeMyEmailRequest req)
        {
            string userName = this.User.FindFirstValue(ClaimTypes.Name);
            Domain.Entities.User user = await repository.FindByNameAsync(userName);
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult> ChangeMyUserName(ChangeMyUserNameRequest req)
        {
            string userName = this.User.FindFirstValue(ClaimTypes.Name);
            Domain.Entities.User user = await repository.FindByNameAsync(userName);
            return BadRequest();
        }
    }
}
