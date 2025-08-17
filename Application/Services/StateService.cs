using Domain.Interface;
using Domain.Entities;
using Application.Interfaces;   

namespace Application.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public IEnumerable<State> SearchStates(string stateAbbr, string stateName)
        {
            return _stateRepository.SearchStates(stateAbbr, stateName);
        }

        public State? GetStateById(int id)
        {
            return _stateRepository.GetStateById(id);
        }

        public IEnumerable<State> GetStates()
        {
            return _stateRepository.GetAllStates();
        }
    }
}