using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileService.Infrastructure.Configs;
public class FileItemConfig : IEntityTypeConfiguration<FileItem>
{
    public void Configure(EntityTypeBuilder<FileItem> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
