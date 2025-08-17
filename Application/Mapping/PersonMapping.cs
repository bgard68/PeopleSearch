using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping
{
    public static class PersonMapping
    {
        public static PersonDto ToDto(Person person)
        {
            if (person == null) return null;
            return new PersonDto
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                MI = person.MI,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                CellNumber = person.CellNumber,
                Email = person.Email,
                AddressId = person.AddressId,
                Address = AddressMapping.ToDto(person.Address)
            };
        }

        public static Person ToEntity(PersonDto dto, Address trackedAddress)
        {
            if (dto == null) return null;
            return new Person
            {
                PersonId = dto.PersonId,
                FirstName = dto.FirstName,
                MI = dto.MI,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                CellNumber = dto.CellNumber,
                Email = dto.Email,
                AddressId = dto.AddressId,
                Address = trackedAddress // Use the tracked Address instance
            };
        }
    }
}