namespace FileService.Domain.Entities;
public record FileIdentifier(string UserName, string ProjectName, string FileType, string VersionName, string FileName);
