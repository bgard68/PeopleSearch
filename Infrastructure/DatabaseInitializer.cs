using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DatabaseInitializer
    {
        public static async Task EnsureDatabaseCreatedAndSeededAsync(AppDbContext dbContext, string baseDirectory)
        {
            // Run migrations if the database does not exist
            if (!dbContext.Database.CanConnect())
            {
                dbContext.Database.Migrate();
            }

            // Import states from CSV file
            await dbContext.ImportStatesFromCsvAsync(Path.Combine(baseDirectory, "states.csv"));
            // Optionally import addresses
            // await dbContext.ImportAddressesFromCsvAsync(Path.Combine(baseDirectory, "addresses.csv"));
        }
    }
}