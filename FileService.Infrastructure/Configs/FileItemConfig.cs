using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileService.Infrastructure.Configs;
public class FileItemConfig : IEntityTypeConfiguration<FileItem>
{
    public void Configure(EntityTypeBuilder<FileItem> builder)
    {
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.OwnsOne(p => p.FileIdentifier, nb =>
        {
            nb.Property(x => x.UserName).IsRequired().HasMaxLength(32).IsUnicode(false).HasColumnName("UserName");
            nb.Property(x => x.ProjectName).IsRequired().HasMaxLength(256).IsUnicode(false).HasColumnName("ProjectName");
            nb.Property(x => x.FileType).IsRequired().HasMaxLength(64).IsUnicode(false).HasColumnName("FileType");
            nb.Property(x => x.VersionName).IsRequired().HasMaxLength(256).IsUnicode(false).HasColumnName("VersionName");
            nb.Property(x => x.FileName).IsRequired().HasMaxLength(256).IsUnicode(true).HasColumnName("FileName");
        });
        builder.Property(e => e.FileSHA256Hash).HasMaxLength(64).IsUnicode(false);
        builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });
    }
}
