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


    public async Task<Project?> GetProjectAsync(string userName, string projectName)
    {
        return await dbContext.Projects
            .Include(p => p.ReadmeFiles)
            .Include(p => p.ProjectVersions)
            .Include(p => p.FirmwareVerisions)
            .Include(p => p.ModelVersions).ThenInclude(m => m.Files)
            .SingleOrDefaultAsync(p => p.UserName == userName && p.Name == projectName);
    }
    public async Task<Project[]?> GetProjectsByUserNameAsync(string userName)
    {
        return await dbContext.Projects.Include(p => p.ReadmeFiles).Where(p => p.UserName == userName).ToArrayAsync();
    }
    public async Task<ProjectVersion?> GetProjectVersionAsync(string userName, string projectName, string versionName)
    {
        return await dbContext.ProjectVersions
            .Include(p => p.FirmwareVersion)//.ThenInclude(f => f.Files)
            .Include(p => p.ModelVersion)//.ThenInclude(f => p.Files)
            .SingleOrDefaultAsync(p => p.Project.UserName == userName && p.Project.Name == projectName && p.Name == versionName);
    }
    public async Task<FirmwareVersion?> GetFirmwareVerisionAsync(string userName, string projectName, string versionName)
    {
        return await dbContext.FirmwareVerisions.Include(p => p.Files).SingleOrDefaultAsync(f => f.Project.UserName == userName && f.Project.Name == projectName && f.Name == versionName);
    }
    public async Task<ModelVersion?> GetModelVersionAsync(string userName, string projectName, string versionName)
    {
        return await dbContext.ModelVersions.Include(p => p.Files).SingleOrDefaultAsync(f => f.Project.UserName == userName && f.Project.Name == projectName && f.Name == versionName);
    }


    public async Task<Project> CreateProjectAsync(string userName, string name, string? description, List<string>? tags)
    {
        var project = new Project(userName, name, description, tags);
        await dbContext.AddAsync(project);
        return project;
    }
    public async Task<ProjectVersion> CreateProjectVersionAsync(Project project, string name, string description, FirmwareVersion firmwareVersion, ModelVersion modelVersion)
    {
        var projectVersion = new ProjectVersion(project, name, description, firmwareVersion, modelVersion);
        await dbContext.AddAsync(projectVersion);
        project.ProjectVersions.Add(projectVersion);
        return projectVersion;
    }
    public async Task<FirmwareVersion> CreateFirmwareVersionAsync(string versionName, Project project)
    {
        var firmwareVersion = new FirmwareVersion(versionName, project);
        await dbContext.AddAsync(firmwareVersion);
        project.FirmwareVerisions.Add(firmwareVersion);
        return firmwareVersion;
    }
    public async Task<ModelVersion> CreateModelVersionAsync(string versionName, Project project)
    {
        var modelVersion = new ModelVersion(versionName, project);
        await dbContext.AddAsync(modelVersion);
        project.ModelVersions.Add(modelVersion);
        return modelVersion;
    }


    public void RemoveProject(Project project)
    {
        dbContext.Projects.Remove(project);
    }
    public void RemoveProjectVersion(ProjectVersion projectVersion)
    {
        dbContext.ProjectVersions.Remove(projectVersion);
    }
    public void RemoveFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        dbContext.FirmwareVerisions.Remove(firmwareVersion);
    }
    public void RemoveModelVersion(ModelVersion modelVersion)
    {
        dbContext.ModelVersions.Remove(modelVersion);
    }


    public async Task<ProjectFile?> FindProjectFileAsync(string relativePath)
    {
        return await dbContext.ProjectFiles.SingleOrDefaultAsync(p => p.RelativePath == relativePath);
    }
    public async Task<ProjectFile> CreateProjectFileAsync(ProjectFile file)
    {
       await dbContext.ProjectFiles.AddAsync(file);
       return file;
    }
    public async Task<bool> DeleteProjectFileAsync(ProjectFile file)
    {
        dbContext.ProjectFiles.Remove(file);
        return true;
    }
}
 