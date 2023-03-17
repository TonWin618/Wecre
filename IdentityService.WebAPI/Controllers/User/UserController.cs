using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.WebAPI.Controllers.User
{
    [Route("api/[controller]/[action]")]
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
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (!await repository.VerifyEmailTokenAsync(user, req.token))
            {
                return BadRequest("Token invalid");
            }
            if(IdentityResult.Success != await repository.UpdatePassword(user,req.newPassword))
            {
                return BadRequest("Password change failed");
            };
            return Ok("Your password has changed");
        }
        [HttpPost]
        public async Task<ActionResult> ChangeMyUserName(ChangeMyUserNameRequest req)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (!await repository.VerifyEmailTokenAsync(user, req.token))
            {
                return BadRequest("Token invalid");
            }
            if (null != await repository.FindByNameAsync(req.newUserName))
            {
                return BadRequest("The user name you have selected is already taken");
            }
            if (IdentityResult.Success != await repository.UpdateUserName(user, req.newUserName))
            {
                return BadRequest("User name change failed");
            };
            return Ok("Your user name has changed");
        }
        [HttpPost]
        public async Task<ActionResult> ChangeMyEmail(ChangeMyEmailRequest req)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (!await repository.VerifyEmailTokenAsync(user, req.token))
            {
                return BadRequest("Token invalid");
            }
            if (IdentityResult.Success != await repository.UpdateEmail(user, req.newEmail))
            {
                return BadRequest("User name change failed");
            };
            return Ok("Your user name has changed");
        }
        [HttpPost]
        public async Task<ActionResult> SendConfirmationEmail()
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (await domainService.SendEmailConfirmTokenAsync(user))
            {
                return Ok("Confirmation email was sent successfully");
            }
            else
            {
                return BadRequest("Confirmation email failed to send");
            }
        }
        [HttpPost]
        public async Task<ActionResult> ConfirmEmailAddress(string token)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (IdentityResult.Success == await repository.VerifyConfirmEmailTokenAsync(user,token))
            {
                return Ok("The email address has been confirmed");
            }
            else
            {
                return BadRequest("Failed to confirm the email address");
            }
        }
        [HttpPost]
        public async Task<ActionResult> SendEmailToken()
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (await domainService.SendEmailTokenAsync(user))
            {
                return Ok();
            };
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult> CheckEmailToken(string token)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await repository.FindByNameAsync(userName);
            if (await repository.VerifyEmailTokenAsync(user, token))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
