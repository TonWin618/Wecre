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
        const string restfulUrl = "{userName}/{projectName}/model/{modelVersionName}";
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
            if (null == project) { return NotFound(); }
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
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (null == modelVersion){ return NotFound(); }
            modelVersion.ChangeDonwloads();
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> DeleteProjectVersion(string userName, string projectName, string modelVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (modelVersion == null) { return NotFound(); }
            domainService.DeleteModelVersion(modelVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route($"{restfulUrl}/file")]
        public async Task<ActionResult> CreateFiles(string userName, string projectName, string modelVersionName,
            [FromForm] List<string> descriptions, [FromForm] List<IFormFile> files)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (modelVersion == null) { return NotFound(); }
            //This is duplicate code and needs to be included in a function
            foreach (var item in files.Zip(descriptions, (file, description) => (file, description)))
            {

                string fileName = item.file.FileName;
                string relativePath = $"{userName}/{projectName}/model/{modelVersion.Name}/{fileName}";

                if (null != await repository.FindProjectFileAsync(relativePath))
                {
                    return BadRequest("the target file already exists. ");
                }
                var projectFile = await domainService.CreateFileAsync(item.file.OpenReadStream(), fileName, relativePath, item.description);
                if (null == projectFile)
                {
                    return Problem("File server error. ");
                }
                modelVersion.Files.Add(projectFile);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route($"{restfulUrl}/file")]
        public async Task<ActionResult> DeleteFiles(string userName, string projectName, string modelVersionName,
            List<string> fileNames)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            ModelVersion? modelVersion = await repository.GetModelVersionAsync(userName, projectName, modelVersionName);
            if (modelVersion == null) { return NotFound(); }
            foreach (var fileName in fileNames)
            {
                ProjectFile? file = modelVersion.Files.SingleOrDefault(f => f.Name == fileName);
                if (null == file) { return NotFound(); }
                await domainService.RemoveFileAsync(file);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}