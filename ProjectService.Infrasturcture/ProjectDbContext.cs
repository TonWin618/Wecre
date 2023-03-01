using Microsoft.EntityFrameworkCore;
using ProjectService.Domain.Entities;

namespace ProjectService.Infrasturcture;
public class ProjectDbContext:DbContext
{
    public DbSet<ProjectItem> projectItems { get;private set; }
    public DbSet<VersionItem> versionItems { get;private set; }
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}