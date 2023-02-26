using FileService.Domain.Entities;

namespace FileService.WebAPI.Controllers;

public class UploadRequest
{
    public string ProjectName { get; set; }
    public string FileType { get; set; }
    public string VersionName { get; set; }
    public IFormFile File { get; set; }
}
