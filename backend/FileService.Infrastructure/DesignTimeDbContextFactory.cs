using Common.Initializer;
using FileService.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FileDbContext>
{
    public FileDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<FileDbContext>();
        return new FileDbContext(optionsBuilder.Options);
    }
}