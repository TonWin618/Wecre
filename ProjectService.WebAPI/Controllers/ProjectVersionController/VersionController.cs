using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectService.WebAPI.Controllers.VersionController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetProjectVersion(string projectName, string version)
        {
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateProjectVersion(CreateVersionRequest req)
        {
            return NotFound();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateProjectVersion()
        {
            return NotFound();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteProjectVersion()
        {
            return NotFound();
        }
    }
}