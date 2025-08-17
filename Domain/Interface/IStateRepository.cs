using Domain.Entities;

using System;

public interface IStateRepository
    {
    IEnumerable<State> SearchStates(string stateAbbr, string stateName);
    State? GetStateById(int id);
    IEnumerable<State> GetAllStates();
} 

