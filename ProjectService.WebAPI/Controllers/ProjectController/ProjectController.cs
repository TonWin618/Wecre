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
        [Route("{userName}/{projectName}")]
        public async Task<ActionResult<Project>> GetProject(string userName, string projectName)
        {
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if(project == null) 
            {
                return NotFound();
            }
            //TODO: return view built on Project
            return project;
        }

        [HttpPost]
        [Authorize]
        [Route("{userName}/{projectName}")]
        public async Task<ActionResult> CreateProject(string userName, string projectName,
            UpdateProjectRequest req)
        {
            if(userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid("illegal request. ");
            };
            if(null != await repository.GetProjectAsync(userName, projectName))
            {
                return BadRequest("the target project already exists. ");
            }
            await repository.CreateProjectAsync(userName, projectName, req.Description, req.Tags, null);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("{userName}/{projectName}")]
        public async Task<ActionResult> UpdateProject(string userName,string projectName,
            UpdateProjectRequest req)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound("illegal request. ");
            };
            Project? project = await repository.GetProjectAsync(userName,projectName);
            if(project == null) { return NotFound(); }

            project.ChangeDescription(req.Description);
            project.ChangeTags(req.Tags);
            project.ChangeReadmeFiles(null);

            dbContext.Update(project);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{userName}/{projectName}")]
        public async Task<ActionResult> DeleteProject(string userName, string projectName)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound("illegal request. ");
            };
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (project == null) { return NotFound(); }
            await domainService.DeleteProject(project);
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

        [HttpPost]
        [Authorize]
        //TODO: If the path has the same name as the version controller method.
        [Route("{userName}/{projectName}/readme")]
        public async Task<ActionResult> UpdateFiles(string userName,string projectName,[FromForm]List<string> descriptions, [FromForm]List<IFormFile> files)
        {
            if (userName != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound("illegal request. ");
            };
            Project? project = await repository.GetProjectAsync(userName, projectName);
            if (null == project)
            {
                return NotFound("illegal request. ");
            }
            List<ProjectFile> projectFiles = new();
            foreach(var item in files.Zip(descriptions,(file,description)=>(file,description)))
            {
                string fullPath = $"{userName}/{projectName}/{item.file.FileName}";
                var projectFile = await domainService.CreateFileAsync(item.file.OpenReadStream(), fullPath, item.file.FileName, item.description);
                projectFiles.Add(projectFile);
            }
            project.ChangeReadmeFiles(projectFiles);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
