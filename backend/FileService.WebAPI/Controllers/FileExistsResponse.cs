namespace FileService.WebAPI.Controllers;
public record FileExistsResponse(bool IsExists, Uri? Url);