using Domain.Interface;
using Domain.Entities;
using Infrastructure.Data;
using System.Windows.Forms.VisualStyles;

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
            .Where(p =>
                (string.IsNullOrEmpty(streetName) || p.StreetAddress.Contains(streetName)) &&
                (string.IsNullOrEmpty(cityName) || p.City.Contains(cityName)) && 
                (stateId == 0 || p.StateId == stateId) &&
                (string.IsNullOrEmpty(zipCode) || p.ZipCode.Contains(zipCode)))
            .ToList();
    }

    public Address? GetAddressById(int id)
    {
        return _context.Addresses.Find(id);
    }

    public IEnumerable<Address> GetAllAddresses()
    {
        return _context.Addresses.ToList();
    }

    public void AddAddress(Address address)
    {
        _context.Addresses.Add (address);
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



