using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;
internal class ModelVersionConfig : IEntityTypeConfiguration<ModelVersion>
{
    public void Configure(EntityTypeBuilder<ModelVersion> builder)
    {
        builder.HasOne(c => c.Project).WithMany(a => a.ModelVersions);
    }
}
