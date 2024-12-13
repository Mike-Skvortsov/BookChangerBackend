using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;


namespace Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            // Визначення шляху до appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../BooksChanger");

            // Створення конфігурації для читання appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Вказівка на директорію BooksChanger
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Отримання рядка підключення з appsettings.json
            var connectionString = configuration.GetConnectionString("DB");

            // Налаштування DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseNpgsql(connectionString);

            return new Context(optionsBuilder.Options);
        }
    }
}

