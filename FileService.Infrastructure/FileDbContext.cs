using FileService.Domain;
using FileService.Domain.Entities;
using FileService.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure;

public class FileDbContext:DbContext
{
    public DbSet<FileItem> FileItems { get; private set; }
    public FileDbContext(DbContextOptions options) : base(options) 
    { 

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
