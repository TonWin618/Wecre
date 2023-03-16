using ProjectService.Domain.Entities;
using System.Net.Http;
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
    public async Task<bool> DeleteAllFileAsync(Project project, CancellationToken stoppingToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        if (null == project.ReadmeFiles) return true;
        foreach (ProjectFile item in project.ReadmeFiles)
        {
            Uri deleteUrl = new(fileServerRoot, $"api/Uploader/Delete?relativePath={item.RelativePath}");
            HttpResponseMessage deleteResp = await httpClient.PostAsync(deleteUrl, null, stoppingToken);
            if (!deleteResp.IsSuccessStatusCode)
            {
                return false;
            }
        }
        return true;
    }
    public async Task<ProjectFile?> CreateFileAsync(Stream file, string fileName, string relativePath, string description, CancellationToken stoppingToken = default)
    {
        using MultipartFormDataContent content= new MultipartFormDataContent();
        var fileContent = new StreamContent(file);
        content.Add(fileContent, "file", relativePath);
        var httpClient = httpClientFactory.CreateClient();
        Uri createUrl = new(fileServerRoot, $"api/Uploader/Upload?relativePath={relativePath}");
        HttpResponseMessage createResp = await httpClient.PostAsync(createUrl, content, stoppingToken);
        UploadResp? json = await createResp.Content.ReadFromJsonAsync<UploadResp>();
        if (createResp.IsSuccessStatusCode)
        {
            var projectFile = ProjectFile.Create(name: fileName, relativePath: relativePath, sizeInBytes: json.FileSize, description: description);
            return await repository.CreateProjectFileAsync(projectFile);
        }
        return null;
    }

    public async Task<bool> RemoveFileAsync(ProjectFile file, CancellationToken stoppingToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        Uri deleteUrl = new(fileServerRoot, $"api/Uploader/Delete?relativePath={file.RelativePath}");
        HttpResponseMessage deleteResp = await httpClient.DeleteAsync(deleteUrl, stoppingToken);
        if(!deleteResp.IsSuccessStatusCode) 
        {
            return false;
        }
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
                await RemoveFileAsync(file);
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
            await RemoveFileAsync(file);
        }
        repository.RemoveFirmwareVersion(firmwareVersion);
    }

    public async void DeleteModelVersion(ModelVersion modelVersion)
    {
        foreach (var file in modelVersion.Files)
        {
            await RemoveFileAsync(file);
        }
        repository.RemoveModelVersion(modelVersion);
    }
}
