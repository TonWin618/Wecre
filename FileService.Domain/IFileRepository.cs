using FileService.Domain.Entities;

namespace FileService.Domain;
public interface IFileRepository
{
    Task<UploadItem> FindFileAsync(long fileSize, string sha256Hash);
}
