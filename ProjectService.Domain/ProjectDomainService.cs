using ProjectService.Domain.Entities;
using System.Net.Http.Json;

namespace ProjectService.Domain;

public class ProjectDomainService
{
    private readonly Uri fileServerRoot;
    private readonly IProjectRepository repository;
    private readonly IHttpClientFactory httpClientFactory;
    public ProjectDomainService(IProjectRepository repository, IHttpClientFactory httpClientFactory, Uri fileServerRoot)
    {
        this.repository = repository;
        this.httpClientFactory = httpClientFactory;
        this.fileServerRoot= fileServerRoot;
    }

    public async Task<ProjectFile?> CreateFileAsync(FileInfo file, string dictory, string fileName, string description, CancellationToken stoppingToken = default)
    {
        using MultipartFormDataContent content= new MultipartFormDataContent();
        using var fileContent = new StreamContent(file.OpenRead());
        content.Add(fileContent, "file", file.Name);
        var httpClient = httpClientFactory.CreateClient();
        Uri requestUrl = new(fileServerRoot, $"api/Uploader/Upload");
        var resp = await httpClient.PostAsync(requestUrl,content , stoppingToken);
        if(resp.IsSuccessStatusCode)
        {
            var projectFile = ProjectFile.Create(name: fileName, url: dictory, sizeInBytes: 0, description: description);
            return await repository.CreateFileAsync(projectFile);
        }
        return null;
    }

    public async Task<bool> RemoveFileAsync(string relativePath, CancellationToken stoppingToken = default)
    {
        Uri requestUrl = new(fileServerRoot, $"api/Uploader/RemoveFile?relativePath={relativePath}");
        var httpClient = httpClientFactory.CreateClient();
        await httpClient.GetFromJsonAsync<bool>(requestUrl, stoppingToken);
        var file = await repository.FindFileAsync(relativePath);
        await repository.DeleteFileAsync(file);
        return true;
    }

    public async Task DeleteProject(Project project)
    {   
        if(project.FirmwareVerisions!= null)
        {
            foreach (var firmwareVersion in project.FirmwareVerisions)
            {
                DeleteFirmwareVersion(firmwareVersion);
            }
        }
        if(project.ModelVersions!=null)
        {
            foreach (var modelVersion in project.ModelVersions)
            {
                foreach (var file in modelVersion.Files)
                {
                    DeleteModelVersion(modelVersion);
                }
            }
        }
        if(project.ReadmeFiles!=null)
        {
            foreach (var file in project.ReadmeFiles)
            {
                RemoveFileAsync(file.Url);
            }
        }
        repository.RemoveProject(project);
        
    }

    public void DeleteProjectVersion(ProjectVersion projectVersion)
    {
        repository.RemoveProjectVersion(projectVersion);
    }

    public void DeleteFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        foreach(var file in firmwareVersion.Files)
        {
            RemoveFileAsync(file.Url);
        }
        //repository.RemoveFirmwareVersion(firmwareVersion);
    }

    public void DeleteModelVersion(ModelVersion modelVersion)
    {
        foreach (var file in modelVersion.Files)
        {
            RemoveFileAsync(file.Url);
        }
        //repository.RemoveModelVersion(modelVersion);
    }
}
