using Domain.Entities;

public interface IPeopleRepository
    {
        IEnumerable<Person> Search(string firstName, string mi, string lastName);
        Person? GetById(int id);
        IEnumerable<Person> GetAllPeople();
    } 
