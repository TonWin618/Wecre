using Microsoft.EntityFrameworkCore;

namespace Common.Initializer
{
    public class DbContextOptionsBuilderFactory
    {
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            var connStr = Environment.GetEnvironmentVariable("DefaultDB:ConnStr");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseNpgsql(connStr);
            return optionsBuilder;
        }
    }
}