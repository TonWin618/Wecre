using FileService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileService.Infrastructure.Configs;
public class FileItemConfig : IEntityTypeConfiguration<FileItem>
{
    public void Configure(EntityTypeBuilder<FileItem> builder)
    {
        builder.ToTable("T_FileItems");
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.OwnsOne(p => p.FileIdentifier
        //    nb =>
        //{
        //    nb.Property(x => x.UserName).HasMaxLength(32).IsUnicode(false);
        //    nb.Property(x => x.ProjectName).HasMaxLength(64).IsUnicode(false);
        //    nb.Property(x => x.FileType).HasMaxLength(64).IsUnicode(false);
        //    nb.Property(x => x.VersionName).HasMaxLength(64).IsUnicode(false);
        //    nb.Property(x => x.FileName).HasMaxLength(256).IsUnicode(true);
        );
        builder.Property(e => e.FileSHA256Hash).HasMaxLength(64).IsUnicode(false);
        builder.HasIndex(e => new { e.FileSHA256Hash, e.FileSizeInBytes });
    }
}
