using Application.Interfaces;
using Application.DTOs;
using Application.Mapping;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public IEnumerable<AddressDto> SearchAddress(string streetName, string cityName, int stateId, string zipCode)
        {
            var addresses = _addressRepository.SearchAddress(streetName, cityName, stateId, zipCode);
            return addresses.Select(AddressMapping.ToDto).ToList();
        }

        public AddressDto? GetAddressById(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            return address != null ? AddressMapping.ToDto(address) : null;
        }

        public IEnumerable<AddressDto> GetAllAddresses()
        {
            var addresses = _addressRepository.GetAllAddresses();
            return addresses.Select(AddressMapping.ToDto).ToList();
        }

        public (bool Success, string Message) AddAddress(AddressDto addressDto)
        {
            if (string.IsNullOrWhiteSpace(addressDto.StreetAddress) ||
                string.IsNullOrWhiteSpace(addressDto.City) ||
                addressDto.StateId == 0 ||
                string.IsNullOrWhiteSpace(addressDto.ZipCode))
            {
                return (false, "All required fields must be provided.");
            }
            var address = AddressMapping.ToEntity(addressDto);
            _addressRepository.AddAddress(address);
            return (true, "Address added successfully.");
        }

        public void UpdateAddress(AddressDto addressDto)
        {
            var address = AddressMapping.ToEntity(addressDto);
            _addressRepository.UpdateAddress(address);
        }

        public void DeleteAddress(int id)
        {
            _addressRepository.DeleteAddress(id);
        }
    }
}