using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            var connectionString = Environment.GetEnvironmentVariable("RAILWAY_DATABASE_URL");
            optionsBuilder.UseSqlServer(
                connectionString,
                options => options.EnableRetryOnFailure()
            );

            return new Context(optionsBuilder.Options);
        }

    }
}
