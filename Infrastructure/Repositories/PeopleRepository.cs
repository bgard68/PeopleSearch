using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PeopleRepository : IPeopleRepository
{
    private readonly AppDbContext _context;

    public PeopleRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Person> Search(string firstName, string mi, string lastName)
    {
        return _context.People
            .Where(p =>
                (string.IsNullOrEmpty(firstName) || p.FirstName.Contains(firstName)) &&
                (string.IsNullOrEmpty(mi) || p.MI.Contains(mi)) &&
                (string.IsNullOrEmpty(lastName) || p.LastName.Contains(lastName)))
            .ToList();
    }

    // Example for a repository method
    public IEnumerable<Person> GetAllPeople()
    {
        return _context.People
        .Include(p => p.Address)
        .ThenInclude(a => a.State)
        .ToList();
    }

    public Person? GetById(int id)
    {
        return _context.People
            .Include(p => p.Address)
            .FirstOrDefault(p => p.PersonId == id);
    }

    public void Add(Person person)
    {
        _context.People.Add(person);
        _context.SaveChanges();
    }

    public void Update(Person person)
    {
        _context.People.Update(person);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var person = _context.People.Find(id);
        if (person != null)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }
    }
}