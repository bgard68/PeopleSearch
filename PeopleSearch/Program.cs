using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;  
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows.Forms;

namespace PeopleSearch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Standard WinForms startup configuration
            ApplicationConfiguration.Initialize(); // This should be done early
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // 1. Build the Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Use AppContext.BaseDirectory for reliable path in WinForms
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Build the Service Collection (DI container)
            var serviceCollection = new ServiceCollection();

            // Register configuration
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // Register DbContext
            // Do Not change the name "DefaultConnection" unless you have a different connection string name in appsettings.json
            // If you do change it, make sure to update the connection string in appsettings.json accordingly and Dbcontext and
            // in the DesignTimeDbContextFactory class in the infrastructure project if you are using EF Core migrations.
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
            }

            // Example for SQL Server
            serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Example for SQLite (uncomment if needed)
            // serviceCollection.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlite(connectionString));

            // Register State Service
            serviceCollection.AddScoped<IStateRepository, StateRepository>();
            serviceCollection.AddScoped<IStateService, StateService>();

                  
            // Assuming PeopleService requires AppDbContext in its constructor
            serviceCollection.AddScoped<IPeopleRepository, PeopleRepository>();
            serviceCollection.AddScoped<IPeopleService, PeopleService>();

            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<IAddressService, AddressService>();

            // 3. Build the Service Provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // 4. Resolve the main form and run the application
            // It's generally good practice to resolve the form within a scope,
            // especially if services within the form are scoped.
            using (var scope = serviceProvider.CreateScope())
            {
                // Ensure database is created and migrations are applied
                // This will create the database if it does not exist and apply any pending migrations
                // Be sure that you do not already have an existing database defined in the connection string!!!
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                // Only run migrations if the database does not exist
                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.Migrate();
                }

                // Import states from CSV file
                dbContext.ImportStatesFromCsvAsync(Path.Combine(AppContext.BaseDirectory, "states.csv")).GetAwaiter().GetResult();
                // dbContext.ImportAddressesFromCsvAsync(Path.Combine(AppContext.BaseDirectory, "addresses.csv")).GetAwaiter().GetResult();

                // Retrieve the service (or the Form itself if Form1 was registered with DI)
                // If Form1 has dependencies, you'd typically register Form1 itself and resolve it:
                // serviceCollection.AddScoped<Form1>(); // Add this if Form1 itself has dependencies to be injected
                // var form1 = scope.ServiceProvider.GetRequiredService<Form1>();

                // Since Form1 only takes IPeopleService, we can resolve IPeopleService directly and pass it:
                var stateService = scope.ServiceProvider.GetRequiredService<IStateService>();
                var peopleService = scope.ServiceProvider.GetRequiredService<IPeopleService>();
                var addressService = scope.ServiceProvider.GetRequiredService<IAddressService>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var peopleSearchForm = new PeopleSearchForm(stateService, peopleService,addressService, config);
                // Comment out to check for any processing before handling form events
                // peopleSearchForm.ShowDialog();
                System.Windows.Forms.Application.Run(peopleSearchForm);
            }
        }
    }
}