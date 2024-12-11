using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(
                "Server=database;Database=ProjectWithMyLove;User=sa;Password=#qawsed123SS;",
                options => options.EnableRetryOnFailure());

            return new Context(optionsBuilder.Options);
        }
    }
}
