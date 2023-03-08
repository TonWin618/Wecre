namespace FileService.WebAPI.Controllers;
public record FileUploadResponse(long FileSize, Uri? Url);