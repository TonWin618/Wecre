using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture.Configs;
internal class ProjectFileConfig : IEntityTypeConfiguration<ProjectFile>
{
    public void Configure(EntityTypeBuilder<ProjectFile> builder)
    {

    }
}
