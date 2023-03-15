using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using ProjectService.Domain.Entities;
using ProjectService.Infrasturcture;
using System.Security.Claims;

namespace ProjectService.WebAPI.Controllers.ProjectController
{
    [Route("api/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        const string restfulUrl = "{userName}/{projectName}";
        private readonly ProjectDbContext dbContext;
        private readonly IProjectRepository repository;
        private readonly ProjectDomainService domainService;
        public ProjectController(ProjectDbContext dbContext, IProjectRepository repository, ProjectDomainService domainService)
        {
            this.dbContext = dbContext;
            this.repository = repository;
            this.domainService = domainService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(restfulUrl)]
        public async Task<ActionResult<Project>> GetProject(string userName, string projectName)
        {
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (project == null) { return NotFound(); }
            //TODO: return view built on Project
            return project;
        }

        [HttpPost]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> CreateProject(string userName, string projectName,
            string? description, List<string>? tags)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            if (null != await repository.GetProjectAsync(userName, projectName))
            {
                return Conflict("the target project already exists. ");
            }

            await repository.CreateProjectAsync(userName, projectName, description, tags);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> UpdateProject(string userName,string projectName,
            string? description, List<string>? tags)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            Project? project = await repository.GetProjectAsync(userName,projectName);
            if(project == null) { return NotFound(); }

            project.ChangeDescription(description);
            project.ChangeTags(tags);

            dbContext.Update(project);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route(restfulUrl)]
        public async Task<ActionResult> DeleteProject(string userName, string projectName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)) { return BadRequest(); }
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (project == null) { return NotFound(); }

            await domainService.DeleteProject(project);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{userName}/{projectName}/readme")]
        public async Task<ActionResult> UpdateFiles(string userName,string projectName,
            [FromForm]List<string> descriptions, [FromForm]List<IFormFile> files)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier)){ return BadRequest(); }
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (project == null) { return NotFound(); }

            //remove duplicated files
            List<ProjectFile> projectFiles = new();
            foreach(var item in files.Zip(descriptions,(file,description)=>(file,description)))
            {
                string fileName = item.file.FileName;
                string fullPath = $"{userName}/{projectName}/{fileName}";
                var projectFile = await domainService.CreateFileAsync(item.file.OpenReadStream(), fileName, fullPath, item.description);
                if(projectFile == null)
                {
                    return Problem("File server error. ");
                }
                projectFiles.Add(projectFile);
            }
            project.ChangeReadmeFiles(projectFiles);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userName}")]
        public async Task<ActionResult<Project[]?>> GetProjects(string userName)
        {
            //TODO: return view built on Project
            return await repository.GetProjectsByUserNameAsync(userName); ;
        }
    }
}
