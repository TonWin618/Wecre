using FileService.Domain;
using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure;

public class FileRepository:IFileRepository
{
    private readonly FileDbContext dbContext;
    public FileRepository(FileDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<UploadItem> FindFileAsync(long fileSize, string sha256Hash)
    {
        return await dbContext.UploadItems.FirstOrDefaultAsync(
            u => u.FileSizeInBytes == fileSize && u.FileSHA256Hash == sha256Hash);
    }
}
