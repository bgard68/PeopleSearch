// Remove this line, it's not needed and can cause issues in .NET Core/8 projects
// using System.DirectoryServices.ActiveDirectory;

using System.Globalization;
using System.IO;
using CsvHelper;
using Domain.Entities;


namespace Infrastructure.Data
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<State> States { get; set; } // Add this line to make States DbSet accessible
        public DbSet<Address> Addresses { get; set; } // Add this line to make Addresses DbSet accessible
                                                      //  public DbSet<Address> Addresses { get; set; } // Add this line to make Addresses DbSet accessible

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<State>()
               .HasIndex(s => s.StateName)
               .IsUnique();

            modelBuilder.Entity<State>()
                .HasIndex(s => s.StateAbbr)
                .IsUnique();


            // Address configuration
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.State)
                .WithMany()
                .HasForeignKey(a => a.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person configuration
            modelBuilder.Entity<Person>().HasKey(p => p.PersonId);
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithMany() // <--- This means many people can share one address
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
                    }



        public async Task ImportStatesFromCsvAsync(string csvFilePath)
        {
            // Only import if the States table is empty
            if (await States.AnyAsync())
                return;

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

        //public async Task ImportAddressesFromCsvAsync(string addressCsvPath)
        //{
        //    using var reader = new StreamReader(addressCsvPath);
        //    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        //    var addressRecords = csv.GetRecords<AddressCsvModel>().ToList();

        //    foreach (var record in addressRecords)
        //    {
        //        var state = States.FirstOrDefault(s => s.StateAbbr == record.StateAbbr);
        //        if (state != null)
        //        {
        //            var address = new Address
        //            {
        //                StreetAddress = record.StreetAddress,
        //                City = record.City,
        //                StateId = state.StateId,
        //                ZipCode = record.ZipCode
        //            };
        //            Addresses.Add(address);
        //        }
        //    }
        //    await SaveChangesAsync();
        //}
    }
}

