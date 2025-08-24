using Domain.Entities;
using Application.DTOs;
using Application.DTOs.Interfaces;

namespace Application.Mapping
{
    public static class AddressMapping
    {
        public static HomeAddressDto ToDto(Address address)
        {
            if (address == null) return null;
            return new HomeAddressDto
            {
                AddressId = address.AddressId,
                StreetAddress = address.StreetAddress,
                City = address.City,
                StateId = address.StateId,
                ZipCode = address.ZipCode,
                State = StateMapping.ToDto(address.State)
            };
        }

        public static Address ToEntity(IAddressDto dto)
        {
            if (dto == null) return null;
            return new Address
            {
                AddressId = dto.AddressId,
                StreetAddress = dto.StreetAddress,
                City = dto.City,
                StateId = dto.StateId,
                ZipCode = dto.ZipCode,
                State = StateMapping.ToEntity(dto.State)
            };
        }
    }
}