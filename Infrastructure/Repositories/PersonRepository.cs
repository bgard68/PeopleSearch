using Domain.Interface;
using Domain.Entities;
using Infrastructure.Data;

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

    public Person? GetById(int id)
    {
        return _context.People.Find(id);
    }

    public IEnumerable<Person> GetAllPeople()
    {
        return _context.People.ToList();
    }
}   