using Microsoft.EntityFrameworkCore;
using ProjectService.Domain;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture;

public class ProjectRepository: IProjectRepository
{
    private readonly ProjectDbContext dbContext;

    public ProjectRepository(ProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateFirmwareVersionAsync(string versionName, Project project, List<Guid> files)
    {
        var firmwareVersion = FirmwareVersion.Create(versionName, project, files);
        await dbContext.AddAsync(firmwareVersion);
    }

    public async Task CreateModelVersionAsync(string versionName, Project project, List<Guid> files)
    {
        var modelVersion = ModelVersion.Create(versionName, project, files);
        await dbContext.AddAsync(modelVersion);
    }

    public async Task CreateProjectAsync(string userName, string name, string? description, List<string>? tags, List<Guid> readmeFiles)
    {
        var project = Project.Create(userName, name, description, tags, readmeFiles);
        await dbContext.AddAsync(project);
    }

    public async Task CreateProjectVersionAsync(Project project, string name, string description, FirmwareVersion firmwareVersion, ModelVersion modelVersion)
    {
        var projectVersion = ProjectVersion.Create(project,name,description,firmwareVersion,modelVersion);
        await dbContext.AddAsync(projectVersion);
    }

    public Task<FirmwareVersion?> GetFirmwareVerisionAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ModelVersion?> GetModelVersionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Project?> GetProjectAsync(string userName, string projectName)
    {
        return await dbContext.Projects.SingleOrDefaultAsync(p => p.UserName == userName && p.Name == projectName);
    }

    public async Task<Project[]?> GetProjectsByUserNameAsync(string userName)
    {
        return await dbContext.Projects.Where(p => p.UserName == userName).ToArrayAsync();
    }

    public Task<ProjectVersion?> GetProjectVersionAsync()
    {
        throw new NotImplementedException();
    }

    public void RemoveProject(Project project)
    {
        dbContext.Projects.Remove(project);
    }
}
 