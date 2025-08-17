using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IPeopleService
    {
        IEnumerable<Person> Search(string firstName, string mi, string lastName);
        Person? GetById(int id);

        IEnumerable<Person> GetAllPeople();
        (bool Success, string Message) AddPerson(Person person);
        void UpdatePerson(Person person); // Add this line
        void DeletePerson(int id); // Add this line


    }
}




