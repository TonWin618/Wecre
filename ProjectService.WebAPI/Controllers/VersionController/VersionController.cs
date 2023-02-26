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
        public async Task<ActionResult> GetVersion(string projectName, string version)
        {
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateVersion(CreateVersionRequest req)
        {
            return NotFound();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateVersion()
        {
            return NotFound();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteVersion()
        {
            return NotFound();
        }
    }
}