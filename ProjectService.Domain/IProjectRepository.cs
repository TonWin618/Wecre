using ProjectService.Domain.Entities;

namespace ProjectService.Domain;

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(string userName, string projectName);
    Task<Project[]?> GetProjectsByUserNameAsync(string userName);
    Task CreateProjectAsync(string userName, string name, string? description, List<string>? tags, List<ProjectFile> readmeFiles);
    Task CreateProjectVersionAsync(Project project, string name, string description,
        FirmwareVersion firmwareVersion, ModelVersion modelVersion);
    Task CreateFirmwareVersionAsync(string versionName, Project project, List<ProjectFile> files);
    Task CreateModelVersionAsync(string versionName, Project project, List<ProjectFile> files);

    Task<ProjectFile> FindFileAsync(string Url);
    Task<ProjectFile> CreateFileAsync(ProjectFile file);
    Task<bool> DeleteFileAsync(ProjectFile file);

    Task<ProjectVersion?> GetProjectVersionAsync();
    Task<FirmwareVersion?> GetFirmwareVerisionAsync();
    Task<ModelVersion?> GetModelVersionAsync();

    void RemoveProject(Project project);
    //internal void RemoveProjectVersion(ProjectVersion projectVersion);
    //internal void RemoveFirmwareVersion(FirmwareVersion firmwareVersion);
    //internal void RemoveModelVersion(ModelVersion modelVersion);
}
