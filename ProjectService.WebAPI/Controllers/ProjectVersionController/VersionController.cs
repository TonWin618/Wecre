using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using ProjectService.Domain.Entities;
using ProjectService.Infrasturcture;
using System.Security.Claims;

namespace ProjectService.WebAPI.Controllers.VersionController
{
    [Route("api/")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ProjectDbContext dbContext;
        private readonly IProjectRepository repository;
        private readonly ProjectDomainService domainService;
        public VersionController(ProjectDbContext dbContext, IProjectRepository repository, ProjectDomainService domainService)
        {

            this.dbContext = dbContext;
            this.repository = repository;
            this.domainService = domainService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userName}/{projectName}/{versionName}")]
        public async Task<ActionResult<ProjectVersion>> GetProjectVersion(string userName, string projectName, string versionName)
        {
            ProjectVersion? projectVersion = await repository.GetProjectVersionAsync(userName, projectName,versionName);
            if (null == projectVersion)
            {
                return NotFound();
            }
            return projectVersion;
        }

        [HttpPost]
        [Authorize]
        [Route("{userName}/{projectName}/{versionName}")]
        public async Task<ActionResult> CreateProjectVersion(string userName, string projectName, string versionName,
            string description, string firmwareVersionName, string modelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid("illegal request. ");
            };
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (null == project)
            {
                return Forbid("illegal request. ");
            }
            if (null != await repository.GetProjectVersionAsync(userName, projectName, versionName))
            {
                return BadRequest("the target project version already exists. ");
            }
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (null == firmwareVersion)
            {
                return Forbid("illegal request. ");
            }
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (null == modelVersion)
            {
                return Forbid("illegal request. ");
            }
            await repository.CreateProjectVersionAsync(project, versionName, description, firmwareVersion,modelVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("{userName}/{projectName}/{versionName}")]
        public async Task<ActionResult> UpdateProjectVersion(string userName, string projectName, string versionName,
            string description, string firmwareVersionName, string modelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid("illegal request. ");
            };
            if (null == await repository.GetProjectAsync(userName, projectName))
            {
                return Forbid("illegal request. ");
            }
            ProjectVersion? projectVersion = await repository.GetProjectVersionAsync(userName, projectName, versionName);
            if (null == projectVersion)
            {
                return Forbid("illegal request. ");
            }
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (null == firmwareVersion)
            {
                return Forbid("illegal request. ");
            }
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (null == modelVersion)
            {
                return Forbid("illegal request. ");
            }
            projectVersion.ChangeDescription(description)
                .ChangeFirmwareVersion(firmwareVersion)
                .ChangeModelVersion(modelVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{userName}/{projectName}/{versionName}")]
        public async Task<ActionResult> DeleteProjectVersion(string userName, string projectName, string versionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound("illegal request. ");
            };
            ProjectVersion? projectVersion = await repository.GetProjectVersionAsync(userName, projectName, versionName);
            if (projectVersion == null) { return NotFound(); }
            domainService.DeleteProjectVersion(projectVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}