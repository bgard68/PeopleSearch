// Remove this line, it's not needed and can cause issues in .NET Core/8 projects
// using System.DirectoryServices.ActiveDirectory;


namespace Infrastructure.Data
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Person2> People2 { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<CityName> CityNames { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<CityZipCode> CityZipCodes { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Person2>().HasKey(p => p.PersonId);

            // Composite key for CityZipCode
            modelBuilder.Entity<CityZipCode>()
                .HasKey(cz => new { cz.CityId, cz.ZipCodeId });


            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    PersonId = 1,
                    FirstName = "Alice",
                    MI = "B.",
                    LastName = "Smith",
                    PhoneNumber = "123-456-7890",
                    CellNumber = "987-654-3210",
                    Email = "alice.smith@example.com"
                },
                new Person
                {
                    PersonId = 2,
                    FirstName = "Bob",
                    MI = "C.",
                    LastName = "Johnson",
                    PhoneNumber = "555-555-5555",
                    CellNumber = "444-444-4444",
                    Email = "bob.johnson@example.com"
                }
            );
        }
    }
}