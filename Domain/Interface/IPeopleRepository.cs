using Domain.Entities;

namespace Domain.Interface {
    public interface IPeopleRepository
    {
        IEnumerable<Person> Search(string firstName, string mi, string lastName);
        Person? GetById(int id);
        IEnumerable<Person> GetAllPeople();
        void Add(Person person); // Add this line
        void Update(Person person);
        void Delete(int id);
    }
    }