using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Week5.Infrastructure_Layer.Persistence
{
    public class Week5DbContextFactory : IDesignTimeDbContextFactory<Week5DbContext>
    {
        public Week5DbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<Week5DbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("❌ Connection String is not set in configuration.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new Week5DbContext(optionsBuilder.Options, configuration);
        }
    }
}
