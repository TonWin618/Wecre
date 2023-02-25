namespace FileService.WebAPI.Controllers;

public class UploadRequest
{
    public string FileNameWithoutExtension { get; set; }
    public IFormFile File { get; set; }
}
