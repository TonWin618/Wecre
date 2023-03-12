using ProjectService.Domain.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectService.Domain;

public class ProjectDomainService
{
    private readonly Uri fileServerRoot;
    private readonly IProjectRepository repository;
    private readonly IHttpClientFactory httpClientFactory;
    public ProjectDomainService(IProjectRepository repository, IHttpClientFactory httpClientFactory)
    {
        this.repository = repository;
        this.httpClientFactory = httpClientFactory;
        this.fileServerRoot= fileServerRoot = new Uri("https://localhost:7212");
    }
    class UploadResp { public long FileSize { get; set; } public string Url { get; set; } }
    public async Task<ProjectFile?> CreateFileAsync(Stream file, string dictory, string fileName, string description, CancellationToken stoppingToken = default)
    {
        using MultipartFormDataContent content= new MultipartFormDataContent();
        var fileContent = new StreamContent(file);
        content.Add(fileContent, "file", fileName);
        var httpClient = httpClientFactory.CreateClient();
        Uri requestUrl = new(fileServerRoot, $"api/Uploader/Upload?relativePath={dictory}");
        HttpResponseMessage resp = await httpClient.PostAsync(requestUrl,content, stoppingToken);

        UploadResp json = await resp.Content.ReadFromJsonAsync<UploadResp>();

        if (resp.IsSuccessStatusCode)
        {
            var projectFile = ProjectFile.Create(name: fileName, url: json.Url, sizeInBytes: json.FileSize, description: description);
            return await repository.CreateProjectFileAsync(projectFile);
        }
        return null;
    }

    public async Task<bool> RemoveFileAsync(string relativePath, CancellationToken stoppingToken = default)
    {
        Uri requestUrl = new(fileServerRoot, $"api/Uploader/RemoveFile?relativePath={relativePath}");
        var httpClient = httpClientFactory.CreateClient();
        await httpClient.GetFromJsonAsync<bool>(requestUrl, stoppingToken);
        var file = await repository.FindProjectFileAsync(relativePath);
        await repository.DeleteProjectFileAsync(file);
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
                DeleteModelVersion(modelVersion);
            }
        }
        if(project.ReadmeFiles!=null)
        {
            foreach (var file in project.ReadmeFiles)
            {
                await RemoveFileAsync(file.Url);
            }
        }
        repository.RemoveProject(project);
        
    }

    public void DeleteProjectVersion(ProjectVersion projectVersion)
    {
        repository.RemoveProjectVersion(projectVersion);
    }

    public async void DeleteFirmwareVersion(FirmwareVersion firmwareVersion)
    {
        foreach(var file in firmwareVersion.Files)
        {
            await RemoveFileAsync(file.Url);
        }
        //repository.RemoveFirmwareVersion(firmwareVersion);
    }

    public async void DeleteModelVersion(ModelVersion modelVersion)
    {
        foreach (var file in modelVersion.Files)
        {
            await RemoveFileAsync(file.Url);
        }
        //repository.RemoveModelVersion(modelVersion);
    }
}
