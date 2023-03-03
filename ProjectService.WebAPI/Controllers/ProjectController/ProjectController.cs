using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Domain;
using ProjectService.Domain.Entities;
using ProjectService.Infrasturcture;
using System.Security.Claims;

namespace ProjectService.WebAPI.Controllers.ProjectController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDbContext dbContext;
        //private readonly ProjectRepository repository;
        //private readonly ProjectDomainService domainService;
        public ProjectController(ProjectDbContext dbContext)
        {

            this.dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Project>> GetProject(string userName,string projectName)
        {
            Project? project = await dbContext.Projects.SingleOrDefaultAsync(e => e.UserName == userName && e.Name == projectName);
            if(projectName== null) 
            {
                return NotFound();
            }
            return project;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Project[]>> GetProjects(string userName)
        {
            Project[]? projects = await dbContext.Projects.Where(e => e.UserName == userName).ToArrayAsync();
            return projects;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateProject(CreateProjectRequest req)
        {
            string userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var project = Project.Create(userName: userName, Name: req.Name, description: req.Description, 
                tags: req.Tags, readmeFiles: req.ReadmeFiles);
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateProject()
        {
            return NotFound();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteProject()
        {
            return NotFound();
        }
    }
}
