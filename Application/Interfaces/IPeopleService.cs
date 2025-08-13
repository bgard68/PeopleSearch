using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IPeopleService
    {
        IEnumerable<Person> Search(string firstName, string mi, string lastName);
        Person? GetById(int id);

        IEnumerable<Person> GetAllPeople();
    }
}