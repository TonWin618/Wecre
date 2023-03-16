using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using ProjectService.Domain.Entities;
using ProjectService.Infrasturcture;
using System.Security.Claims;

namespace ProjectService.WebAPI.Controllers.FirmwareVersionController
{
    [Route("api/")]
    [ApiController]
    public class FirmwareVersionController : ControllerBase
    {
        //This class needs to be abstracted as an interface
        const string restfulUrl = "{userName}/{projectName}/firmware/{firmwareVersionName}";
        private readonly ProjectDbContext dbContext;
        private readonly IProjectRepository repository;
        private readonly ProjectDomainService domainService;
        public FirmwareVersionController(ProjectDbContext dbContext, IProjectRepository repository, ProjectDomainService domainService)
        {

            this.dbContext = dbContext;
            this.repository = repository;
            this.domainService = domainService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(restfulUrl)]
        public async Task<ActionResult<FirmwareVersion>> GetProjectVersion(string userName, string projectName, string firmwareVersionName)
        {
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (null == firmwareVersion){ return NotFound(); }
            return firmwareVersion;
        }

        [HttpPost]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> CreateProjectVersion(string userName, string projectName, string firmwareVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)){ return BadRequest();}
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (null == project){ return NotFound(); }
            if (null != await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName))
            {
                return BadRequest("the target project version already exists. ");
            }
            await repository.CreateFirmwareVersionAsync(firmwareVersionName, project,null);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> UpdateProjectVersion(string userName, string projectName, string firmwareVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            if (null == await repository.GetProjectAsync(userName, projectName)){ return NotFound();}
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (null == firmwareVersion){ return NotFound(); }
            firmwareVersion.ChangeDonwloads();
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> DeleteProjectVersion(string userName, string projectName, string firmwareVersionName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (firmwareVersion == null) { return NotFound(); }
            domainService.DeleteFirmwareVersion(firmwareVersion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route($"{restfulUrl}/file")]
        public async Task<ActionResult> CreateFiles(string userName, string projectName, string firmwareVersionName,
            [FromForm] List<string> descriptions, [FromForm] List<IFormFile> files)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (firmwareVersion == null) { return NotFound(); }
            //This is duplicate code and needs to be included in a function
            foreach (var item in files.Zip(descriptions, (file, description) => (file, description)))
            {
                
                string fileName = item.file.FileName;
                string relativePath = $"{userName}/{projectName}/firmware/{firmwareVersion.Name}/{fileName}";

                if (null != await repository.FindProjectFileAsync(relativePath))
                {
                    return BadRequest("the target file already exists. ");
                }
                var projectFile = await domainService.CreateFileAsync(item.file.OpenReadStream(), fileName, relativePath, item.description);
                if (null == projectFile)
                {
                    return Problem("File server error. ");
                }
                firmwareVersion.Files.Add(projectFile);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route($"{restfulUrl}/file")]
        public async Task<ActionResult> DeleteFiles(string userName, string projectName, string firmwareVersionName, 
            List<string> fileNames)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            FirmwareVersion? firmwareVersion = await repository.GetFirmwareVerisionAsync(userName, projectName, firmwareVersionName);
            if (firmwareVersion == null) { return NotFound(); }
            foreach(var fileName in fileNames)
            {
                ProjectFile? file = firmwareVersion.Files.SingleOrDefault(f => f.Name == fileName);
                if(null == file) { return NotFound(); }
                await domainService.RemoveFileAsync(file);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        
    }
}
