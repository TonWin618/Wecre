using ProjectService.Domain.Entities;

namespace ProjectService.Domain;

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(string userName, string projectName);
    Task<Project[]?> GetProjectsByUserNameAsync(string userName);
    Task<ProjectVersion?> GetProjectVersionAsync(string userName, string projectName, string versionName);
    Task<FirmwareVersion?> GetFirmwareVerisionAsync(string userName, string projectName, string versionName);
    Task<ModelVersion?> GetModelVersionAsync(string userName, string projectName, string versionName);

    Task CreateProjectAsync(string userName, string name, string? description, List<string>? tags);
    Task CreateProjectVersionAsync(Project project, string name, string description,
        FirmwareVersion firmwareVersion, ModelVersion modelVersion);
    Task CreateFirmwareVersionAsync(string versionName, Project project, List<ProjectFile>? files);
    Task CreateModelVersionAsync(string versionName, Project project, List<ProjectFile>? files);


    void RemoveProject(Project project);
    void RemoveProjectVersion(ProjectVersion projectVersion);
    void RemoveFirmwareVersion(FirmwareVersion firmwareVersion);
    void RemoveModelVersion(ModelVersion modelVersion);


    Task<ProjectFile?> FindProjectFileAsync(string relativePath);
    Task<ProjectFile> CreateProjectFileAsync(ProjectFile file);
    Task<bool> DeleteProjectFileAsync(ProjectFile file);
}
