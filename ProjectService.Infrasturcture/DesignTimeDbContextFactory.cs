using Common.Initializer;
using Microsoft.EntityFrameworkCore.Design;
using ProjectService.Infrasturcture;

namespace IdentityService.Infrastructure;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<ProjectDbContext>
{
    public ProjectDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<ProjectDbContext>();
        return new ProjectDbContext(optionsBuilder.Options);
    }
}