using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        IEnumerable<AddressDto> SearchAddress(string streetName, string cityName, int stateId, string zipCode);
        AddressDto? GetAddressById(int id);

        IEnumerable<AddressDto> GetAllAddresses();
        (bool Success, string Message) AddAddress(AddressDto address);
        void UpdateAddress(AddressDto address); // Add this line
        void DeleteAddress(int id); // Add this line


    }
}

