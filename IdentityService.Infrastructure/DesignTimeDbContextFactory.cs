using Common.Initializer;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.Infrastructure;

public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<IdDbContext>
{
    public IdDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<IdDbContext>();
        return new IdDbContext(optionsBuilder.Options);
    }
}