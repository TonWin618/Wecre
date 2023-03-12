using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;
internal class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id).IsClustered(false);
        builder.HasMany(p => p.ReadmeFiles).WithMany();
        builder.HasIndex(e => new { e.UserName, e.Name });
    }
}
