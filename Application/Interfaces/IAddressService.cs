using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        IEnumerable<Address> SearchAddress(string streetName, string cityName, int stateId, string zipCode);
        Address? GetAddressById(int id);

        IEnumerable<Address> GetAllAddresses();
        (bool Success, string Message) AddAddress(Address address);
        void UpdateAddress(Address address); // Add this line
        void DeleteAddress(int id); // Add this line


    }
}

