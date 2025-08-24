using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        IEnumerable<HomeAddressDto> SearchAddress(string streetName, string cityName, int stateId, string zipCode);
        HomeAddressDto? GetAddressById(int id);

        IEnumerable<HomeAddressDto> GetAllAddresses();
        (bool Success, string Message) AddAddress(HomeAddressDto address);
        void UpdateAddress(HomeAddressDto address); // Add this line
        void DeleteAddress(int id); // Add this line


    }
}

