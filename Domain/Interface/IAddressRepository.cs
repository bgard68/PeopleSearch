using Domain.Entities;

public interface IAddressRepository
{
    IEnumerable<Address> SearchAddress(string streetName, string cityName, int stateId, string zipCode);
    Address? GetAddressById(int id);
    IEnumerable<Address> GetAllAddresses();
    void AddAddress(Address address);
    void UpdateAddress(Address address);
    void DeleteAddress(int id);
}