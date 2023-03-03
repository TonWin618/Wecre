namespace ProjectService.Domain;

public interface IProjectRepository
{
    //Project
    public Task GetProjectAsync();
    public Task GetProjectsByUserNameAsync();
    public Task CreateProjectAsync();
    public Task UpdateProjectAsync();
    public Task DeleteProjectAsync();

    //Project Version
    public Task GetProjectVersionAsync();
    public Task GetProjectVersionsByUserNameAsync();
    public Task CreateProjectVersionAsync();
    public Task UpdateProjectVersionAsync();
    public Task DeleteProjectVersionAsync();

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
