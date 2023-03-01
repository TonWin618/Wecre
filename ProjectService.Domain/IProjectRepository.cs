namespace ProjectService.Domain;

public interface IProjectRepository
{
    //Project
    public Task GetProjectAsync();
    public Task GetProjectsByUserNameAsync();
    public Task CreateProjectAsync();
    public Task UpdateProjectAsync();
    public Task DeleteProjectAsync();

    //Version
    public Task GetVersionAsync();
    public Task GetVersionsByUserNameAsync();
    public Task CreateVersionAsync();
    public Task UpdateVersionAsync();
    public Task DeleteVersionAsync();

    //Firmware Vesion
    public Task GetFirmwareVersionAsync();
    public Task GetFirmwareVersionsByUserNameAsync();
    public Task CreateFirmwareVersionAsync();
    public Task UpdateFirmwareVersionAsync();
    public Task DeleteFirmwareVersionAsync();

    //Model Version
    public Task GetModelVersionAsync();
    public Task GetModelVersionsByUserNameAsync();
    public Task CreateModelVersionAsync();
    public Task UpdateModelVersionAsync();
    public Task DeleteModelVersionAsync();
}
