using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using ProjectService.Domain.Entities;
using ProjectService.Infrasturcture;
using System.Security.Claims;

namespace ProjectService.WebAPI.Controllers.ModelVersionController
{
    [Route("api/")]
    [ApiController]
    public class ModelVersionController : ControllerBase
    {
        const string restfulUrl = "{userName}/{projectName}/m/{modelVersionName}";
        private readonly ProjectDbContext dbContext;
        private readonly IProjectRepository repository;
        private readonly ProjectDomainService domainService;
        public ModelVersionController(ProjectDbContext dbContext, IProjectRepository repository, ProjectDomainService domainService)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.domainService = domainService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(restfulUrl)]
        public async Task<ActionResult<ModelVersion>> GetProjectVersion(string userName, string projectName, string modelVersionName)
        {
            ModelVersion? ModelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (null == ModelVersion) { return NotFound(); }
            return ModelVersion;
        }

        [HttpPost]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> CreateProjectVersion(string userName, string projectName, string modelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (null == await repository.GetProjectAsync(userName, projectName)) { return NotFound(); }
            if (null != await repository.GetModelVersionAsync(userName, projectName, modelVersionName))
            {
                return BadRequest("the target project version already exists. ");
            }
            await repository.CreateModelVersionAsync(modelVersionName, project, null);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> UpdateProjectVersion(string userName, string projectName, string modelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            if (null == await repository.GetProjectAsync(userName, projectName)){ return NotFound(); }
            ModelVersion? ModelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (null == ModelVersion){ return NotFound(); }
            ModelVersion.ChangeDonwloads();
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> DeleteProjectVersion(string userName, string projectName, string ModelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            ModelVersion? ModelVersion = await repository.GetModelVersionAsync(userName, projectName, ModelVersionName);
            if (ModelVersion == null) { return NotFound(); }
            domainService.DeleteModelVersion(ModelVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}