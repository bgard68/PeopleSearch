using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
// using System.Runtime.Remoting.Contexts;
using static System.Net.Mime.MediaTypeNames;
using Infrastructure.Data;

namespace Infrastructure.Data
    {

//    Why EF Core Needs This
// •	At design time(e.g., when you run dotnet ef migrations add), EF Core must instantiate your AppDbContext.
// •	Your context’s constructor requires a DbContextOptions<AppDbContext>, which is normally provided by dependency injection at runtime.
// •	At design time, dependency injection is not available, so EF Core looks for an implementation of IDesignTimeDbContextFactory<TContext>.
// What the Factory Does
// •	The factory manually builds a configuration using appsettings.json (just like your app does at runtime).
// •	It reads the connection string from the configuration.
// •	It creates a DbContextOptions<AppDbContext> using that connection string.
// •	It returns a new instance of AppDbContext with those options.
// Result
// •	EF Core can now create your context, connect to the correct database, and generate/apply migrations—using the same settings as your application.
// ---
// Summary:
// This works because it bridges the gap between runtime dependency injection and design-time tooling, ensuring EF Core can always create your context with the correct configuration.
   
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Build configuration to read appsettings.json
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Fix: Ensure Microsoft.Extensions.Configuration.FileExtensions is referenced
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Read the connection string named "DefaultConnection"
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}