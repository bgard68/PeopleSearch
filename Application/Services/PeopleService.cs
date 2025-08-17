using Domain.Interface;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IAddressService _addressService;

        public PeopleService(IPeopleRepository peopleRepository, IAddressService addressService)
        {
            _peopleRepository = peopleRepository;
            _addressService = addressService;
        }

        public IEnumerable<Person> Search(string firstName, string mi, string lastName)
        {
            return _peopleRepository.Search(firstName, mi, lastName);
        }

        public Person? GetById(int id)
        {
            return _peopleRepository.GetById(id);
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _peopleRepository.GetAllPeople();
        }
        public (bool Success, string Message) AddPerson(Person person)
        {
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

        public void UpdatePerson(Person person)
        {
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