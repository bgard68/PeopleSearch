using Domain.Interface;
using Domain.Entities;
using Application.Interfaces;   

namespace Application.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
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
    }
}