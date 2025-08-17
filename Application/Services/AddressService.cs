using Domain.Interface;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public IEnumerable<Address> SearchAddress(string streetName, string cityName, int stateId, string zipCode)
        {
            return _addressRepository.SearchAddress(streetName, cityName, stateId, zipCode);
        }
      

        public Address? GetAddressById(int id)
        {
            return _addressRepository.GetAddressById(id);
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _addressRepository.GetAllAddresses();
        }
        public (bool Success, string Message) AddAddress(Address address)
        {
            if (string.IsNullOrWhiteSpace(address.StreetAddress) ||
        string.IsNullOrWhiteSpace(address.City) ||
        address.StateId == 0 ||
        string.IsNullOrWhiteSpace(address.ZipCode))
            {
                return (false, "All required fields must be provided.");
            }
            _addressRepository.AddAddress(address);
            return (true, "Address added successfully.");
        }

        public void UpdateAddress(Address address)
        {
            _addressRepository.UpdateAddress(address);
        }

        public void DeleteAddress(int id)
        {
            _addressRepository.DeleteAddress(id);
        }
    }
}