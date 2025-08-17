using System.Globalization;
using System.IO; 
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using Domain.Entities;


public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<State>()
            .HasIndex(s => s.StateName)
            .IsUnique();

        modelBuilder.Entity<State>()
            .HasIndex(s => s.StateAbbr)
            .IsUnique();

        modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
        modelBuilder.Entity<Address>()
            .HasOne(a => a.State)
            .WithMany()
            .HasForeignKey(a => a.StateId)
            .OnDelete(DeleteBehavior.Restrict); // Consider Restrict instead of Cascade

        modelBuilder.Entity<Person>().HasKey(p => p.PersonId);
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Address)
            .WithMany()
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.Restrict); // Consider Restrict instead of Cascade
    }
    public async Task ImportStatesFromCsvAsync(string csvFilePath)
    {
        if (!File.Exists(csvFilePath)) return;
        if (await States.AnyAsync()) return;

        try
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var stateRecords = csv.GetRecords<StateCsvModel>().ToList();
            foreach (var record in stateRecords)
            {
                var state = new State
                {
                    StateName = record.StateName,
                    StateAbbr = record.StateAbbr
                };
                States.Add(state);
            }
            await SaveChangesAsync();
        }
        catch (CsvHelperException csvEx)
        {
            // Handle CSV parsing errors
            // Log or display a message as appropriate
            throw new InvalidOperationException("CSV parsing error during state import.", csvEx);
        }
        catch (IOException ioEx)
        {
            // Handle file I/O errors
            throw new InvalidOperationException("File I/O error during state import.", ioEx);
        }
        catch (DbUpdateException dbEx)
        {
            // Handle database update errors
            throw new InvalidOperationException("Database error during state import.", dbEx);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            throw new InvalidOperationException("Unexpected error during state import.", ex);
        }
    }
}