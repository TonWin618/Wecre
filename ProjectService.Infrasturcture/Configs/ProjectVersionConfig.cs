using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;

internal class ProjectVersionConfig:IEntityTypeConfiguration<ProjectVersion>
{
    public void Configure(EntityTypeBuilder<ProjectVersion> builder)
    {
        builder.HasOne(c => c.Project).WithMany(a => a.ProjectVersions);
    }
}
