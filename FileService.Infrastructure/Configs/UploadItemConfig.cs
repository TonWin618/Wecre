using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileService.Infrastructure.Configs;

public class UploadItemConfig:IEntityTypeConfiguration<UploadItem>
{
    public void Configure(EntityTypeBuilder<UploadItem> builder)
    {
        builder.ToTable("T_FS_UploadedItems");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FileName).IsUnicode().HasMaxLength(1024);
        builder.Property(e => e.FileSHA256Hash).IsUnicode(false).HasMaxLength(64);
        builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });
    }
}
