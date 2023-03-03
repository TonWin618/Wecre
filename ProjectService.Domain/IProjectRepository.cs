using ProjectService.Domain.Entities;

namespace ProjectService.Domain;

public interface IProjectRepository
{
    public Task<Project?> GetProjectAsync(string userName, string projectName);
    public Task<Project[]?> GetProjectsByUserNameAsync(string userName);
    public Task CreateProjectAsync(string userName, string name, string? description, List<string>? tags, List<Guid> readmeFiles);
    public Task CreateProjectVersionAsync(Project project, string name, string description,
        FirmwareVersion firmwareVersion, ModelVersion modelVersion);
    public Task CreateFirmwareVersionAsync(string versionName, Project project, List<Guid> files);
    public Task CreateModelVersionAsync(string versionName, Project project, List<Guid> files);

    public Task<ProjectVersion?> GetProjectVersionAsync();
    public Task<FirmwareVersion?> GetFirmwareVerisionAsync();
    public Task<ModelVersion?> GetModelVersionAsync();

    public void RemoveProject(Project project);
    //internal void RemoveProjectVersion(ProjectVersion projectVersion);
    //internal void RemoveFirmwareVersion(FirmwareVersion firmwareVersion);
    //internal void RemoveModelVersion(ModelVersion modelVersion);
}
