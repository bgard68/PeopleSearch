using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IStateService
    {
        IEnumerable<State> SearchStates(string stateAbbr, string stateName);
        State? GetStateById(int id);

        IEnumerable<State> GetStates();
    }
}