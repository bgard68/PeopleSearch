using Application.Interfaces;
using Application.DTOs;
using Application.Mapping;

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
            var trackedAddress = _addressRepository.GetAddressById(personDto.AddressId);
            var person = PersonMapping.ToEntity(personDto, trackedAddress);
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