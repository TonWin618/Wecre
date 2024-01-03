namespace FileService.WebAPI.Controllers;

public record ChangeFileNameRequest(string newFileName, long fileSize, string sha256Hash);
