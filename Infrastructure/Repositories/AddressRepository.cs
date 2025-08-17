using Domain.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Address> SearchAddress(string streetName, string cityName, int stateId, string zipCode)
        {
            return _context.Addresses
                .Where(a =>
                    (string.IsNullOrEmpty(streetName) || a.StreetAddress.Contains(streetName)) &&
                    (string.IsNullOrEmpty(cityName) || a.City.Contains(cityName)) &&
                    (stateId == 0 || a.StateId == stateId) &&
                    (string.IsNullOrEmpty(zipCode) || a.ZipCode.Contains(zipCode)))
                .Include(a => a.State)
                .ToList();
        }

        public Address? GetAddressById(int id)
        {
            return _context.Addresses
                .Include(a => a.State)
                .FirstOrDefault(a => a.AddressId == id);
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _context.Addresses
                .Include(a => a.State)
                .ToList();
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
            }
        }
    }
}

