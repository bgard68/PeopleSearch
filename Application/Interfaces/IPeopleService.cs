using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IPeopleService
    {
        IEnumerable<PersonDto> Search(string firstName, string mi, string lastName);
        PersonDto? GetById(int id);

        IEnumerable<PersonDto> GetAllPeople();
        (bool Success, string Message) AddPerson(PersonDto person);
        void UpdatePerson(PersonDto person); // Add this line
        void DeletePerson(int id); // Add this line


    }
}




