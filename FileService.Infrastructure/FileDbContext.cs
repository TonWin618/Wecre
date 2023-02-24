using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure;

public class FileDbContext:DbContext
{
    public DbSet<UploadItem> UploadItems { get; private set; }
    public FileDbContext(DbContextOptions<FileDbContext>options) : base(options) 
    { 

    }
}
