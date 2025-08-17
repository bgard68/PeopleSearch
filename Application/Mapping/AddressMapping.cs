using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping
{
    public static class AddressMapping
    {
        public static AddressDto ToDto(Address address)
        {
            if (address == null) return null;
            return new AddressDto
            {
                AddressId = address.AddressId,
                StreetAddress = address.StreetAddress,
                City = address.City,
                StateId = address.StateId,
                ZipCode = address.ZipCode,
                State = StateMapping.ToDto(address.State)
            };
        }

        public static Address ToEntity(AddressDto dto)
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