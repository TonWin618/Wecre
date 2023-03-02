using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;

internal class FirmwareVersionConfig : IEntityTypeConfiguration<FirmwareVerision>
{
    public void Configure(EntityTypeBuilder<FirmwareVerision> builder)
    {
        builder.HasOne(c => c.Project).WithMany(a => a.FirmwareVerisions);
    }
}
