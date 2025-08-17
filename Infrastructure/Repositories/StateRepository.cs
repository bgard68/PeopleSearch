using Domain.Interface;
using Domain.Entities;
using Infrastructure.Data;

public class StateRepository : IStateRepository
{
    private readonly AppDbContext _context;

    public StateRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<State> SearchStates(string stateAbbr, string stateName)
    {
        return _context.States
            .Where(s =>
                (string.IsNullOrEmpty(stateAbbr) || s.StateAbbr.Contains(stateAbbr)) &&
                (string.IsNullOrEmpty(stateName) || s.StateName.Contains(stateName)))
            .ToList();
    }


    public IEnumerable<State> GetAllStates()
    {
        return _context.States.ToList();
    }
    public State? GetStateById(int id)
    {
        return _context.States.Find(id);
    }
}   