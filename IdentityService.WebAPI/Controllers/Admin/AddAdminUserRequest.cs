using System;
namespace IdentityService.WebAPI.Controllers.Admin;

public record AddAdminUserRequest(string userName, string password);

