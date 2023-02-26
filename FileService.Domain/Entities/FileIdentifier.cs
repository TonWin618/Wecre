using Microsoft.EntityFrameworkCore;

namespace FileService.Domain.Entities;
[Owned]
public record FileIdentifier(string UserName, string ProjectName, string FileType, string VersionName, string FileName);
