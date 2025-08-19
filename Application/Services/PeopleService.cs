using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IAddressService _addressService;
        private readonly IAddressRepository _addressRepository;

        public PeopleService(IPeopleRepository peopleRepository, IAddressService addressService, IAddressRepository addressRepository)
        {
            _peopleRepository = peopleRepository;
            _addressService = addressService;
            _addressRepository = addressRepository;
        }

        public IEnumerable<PersonDto> Search(string firstName, string mi, string lastName)
        {
            var people = _peopleRepository.Search(firstName, mi, lastName);
            return people.Select(PersonMapping.ToDto).ToList();
        }

        public PersonDto? GetById(int id)
        {
            var person = _peopleRepository.GetById(id);
            return person != null ? PersonMapping.ToDto(person) : null;
        }

        public IEnumerable<PersonDto> GetAllPeople()
        {
            var people = _peopleRepository.GetAllPeople();
            return people.Select(PersonMapping.ToDto).ToList();
        }

        public (bool Success, string Message) AddPerson(PersonDto personDto)
        {

            // Get the tracked Address entity from the repository
            var trackedAddress = _addressRepository.GetAddressById(personDto.AddressId);
            if (trackedAddress == null)
                return (false, "Address not found.");

            var person = PersonMapping.ToEntity(personDto, trackedAddress);
            if (string.IsNullOrWhiteSpace(person.FirstName) ||
                string.IsNullOrWhiteSpace(person.LastName) ||
                string.IsNullOrWhiteSpace(person.Email) ||
                person.AddressId == 0 ||
                string.IsNullOrWhiteSpace(person.PhoneNumber) ||
                person.Address == null ||
                string.IsNullOrWhiteSpace(person.Address.City) ||
                string.IsNullOrWhiteSpace(person.Address.ZipCode))
            {
                return (false, "All required fields must be provided.");
            }
            _peopleRepository.Add(person);
            return (true, "Person added successfully.");
        }

        public void UpdatePerson(PersonDto personDto)
        {
            var person = _peopleRepository.GetById(personDto.PersonId);
            if (person == null) return;

            // 1. Check if the StateId has changed
            bool stateChanged = person.Address.StateId != personDto.Address.StateId;

            // 2. Search for an existing address entry with the new details
            var matchingAddresses = _addressRepository.SearchAddress(
                personDto.Address.StreetAddress,
                personDto.Address.City,
                personDto.Address.StateId,
                personDto.Address.ZipCode
            ).ToList();

            Address newAddress = null;
            if (stateChanged)
            {
                if (matchingAddresses.Any())
                {
                    // 2) Entry exists: update the AddressId in the person record
                    newAddress = matchingAddresses.First();
                    int oldAddressId = person.AddressId;
                    person.AddressId = newAddress.AddressId;
                    person.Address = newAddress;

                    // 3) If old address isn't used by any other people, delete it
                    bool oldAddressInUse = _peopleRepository.GetAllPeople()
                        .Any(p => p.AddressId == oldAddressId && p.PersonId != person.PersonId);
                    if (!oldAddressInUse)
                    {
                        _addressRepository.DeleteAddress(oldAddressId);
                    }
                }
                else
                {
                    // 4) No entry: add new address and update person
                    var addressToAdd = new Address
                    {
                        StreetAddress = personDto.Address.StreetAddress,
                        City = personDto.Address.City,
                        StateId = personDto.Address.StateId,
                        ZipCode = personDto.Address.ZipCode
                    };
                    _addressRepository.AddAddress(addressToAdd);

                    // Get the newly added address (assume it is returned by search)
                    newAddress = _addressRepository.SearchAddress(
                        addressToAdd.StreetAddress,
                        addressToAdd.City,
                        addressToAdd.StateId,
                        addressToAdd.ZipCode
                    ).FirstOrDefault();

                    int oldAddressId = person.AddressId;
                    if (newAddress != null)
                    {
                        person.AddressId = newAddress.AddressId;
                        person.Address = newAddress;
                    }

                    // 3) If old address isn't used by any other people, delete it
                    bool oldAddressInUse = _peopleRepository.GetAllPeople()
                        .Any(p => p.AddressId == oldAddressId && p.PersonId != person.PersonId);
                    if (!oldAddressInUse)
                    {
                        _addressRepository.DeleteAddress(oldAddressId);
                    }
                }
            }
            else
            {
                // StateId not changed: update other fields as usual
                person.FirstName = personDto.FirstName;
                person.LastName = personDto.LastName;
                person.Email = personDto.Email;
                person.PhoneNumber = personDto.PhoneNumber;
                person.AddressId = personDto.AddressId;
                person.Address = _addressRepository.GetAddressById(personDto.AddressId);
            }

            _peopleRepository.Update(person);
        }

        public void DeletePerson(int personId)
        {
            var person = _peopleRepository.GetById(personId);
            if (person == null)
                return;

            int addressId = person.AddressId;

            _peopleRepository.Delete(personId);

            bool addressInUse = _peopleRepository.GetAllPeople()
                .Any(p => p.AddressId == addressId);

            if (!addressInUse)
            {
                _addressService.DeleteAddress(addressId);
            }
        }
    }
}