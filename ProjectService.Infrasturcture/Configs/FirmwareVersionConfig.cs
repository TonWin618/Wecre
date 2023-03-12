using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;

internal class FirmwareVersionConfig : IEntityTypeConfiguration<FirmwareVersion>
{
    public void Configure(EntityTypeBuilder<FirmwareVersion> builder)
    {
        builder.HasOne(c => c.Project).WithMany(a => a.FirmwareVerisions);
        builder.HasMany(p => p.Files).WithMany();
    }
}
