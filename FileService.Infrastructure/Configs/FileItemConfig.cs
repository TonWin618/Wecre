using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileService.Infrastructure.Configs;
public class FileItemConfig : IEntityTypeConfiguration<FileItem>
{
    public void Configure(EntityTypeBuilder<FileItem> builder)
    {
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.Property(e => e.FileSHA256Hash).HasMaxLength(64).IsUnicode(false);
        builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });
    }
}
