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
        return await dbContext.Projects.SingleOrDefaultAsync(p => p.UserName == userName && p.Name == projectName);
    }
    public async Task<Project[]?> GetProjectsByUserNameAsync(string userName)
    {
        return await dbContext.Projects.Where(p => p.UserName == userName).ToArrayAsync();
    }
    public async Task<ProjectVersion?> GetProjectVersionAsync(string userName, string projectName, string versionName)
    {
        return await dbContext.ProjectVersions.SingleOrDefaultAsync(p => p.Project.UserName == userName && p.Project.Name == projectName && p.Name == versionName);
    }
    public Task<FirmwareVersion?> GetFirmwareVerisionAsync(string userName, string projectName, string versionName)
    {
        throw new NotImplementedException();
    }
    public Task<ModelVersion?> GetModelVersionAsync(string userName, string projectName, string versionName)
    {
        throw new NotImplementedException();
    }


    public async Task CreateProjectAsync(string userName, string name, string? description, List<string>? tags)
    {
        var project = Project.Create(userName, name, description, tags);
        await dbContext.AddAsync(project);
    }
    public async Task CreateProjectVersionAsync(Project project, string name, string description, FirmwareVersion firmwareVersion, ModelVersion modelVersion)
    {
        var projectVersion = ProjectVersion.Create(project, name, description, firmwareVersion, modelVersion);
        await dbContext.AddAsync(projectVersion);
    }
    public async Task CreateFirmwareVersionAsync(string versionName, Project project, List<ProjectFile> files)
    {
        var firmwareVersion = FirmwareVersion.Create(versionName, project, files);
        await dbContext.AddAsync(firmwareVersion);
    }
    public async Task CreateModelVersionAsync(string versionName, Project project, List<ProjectFile> files)
    {
        var modelVersion = ModelVersion.Create(versionName, project, files);
        await dbContext.AddAsync(modelVersion);
    }


    public void RemoveProject(Project project)
    {
        dbContext.Projects.Remove(project);
    }
    public void RemoveProjectVersion(ProjectVersion projectVersion)
    {
        dbContext.ProjectVersions.Remove(projectVersion);
    }
    void IProjectRepository.RemoveFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        throw new NotImplementedException();
    }
    void IProjectRepository.RemoveModelVersion(ModelVersion modelVersion)
    {
        throw new NotImplementedException();
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
 