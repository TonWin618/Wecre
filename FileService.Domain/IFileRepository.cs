using FileService.Domain.Entities;

namespace FileService.Domain;
public interface IFileRepository
{
    Task<FileItem> FindFileAsync(string fileName, string hash);
}
