using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping { 
    public static class StateMapping
    {
        public static StateDto ToDto(State state)
        {
            if (state == null) return null;
            return new StateDto
            {
                StateId = state.StateId,
                StateName = state.StateName,
                StateAbbr = state.StateAbbr
            };
        }

        public static State ToEntity(StateDto dto)
        {
            if (dto == null) return null;
            return new State
            {
                StateId = dto.StateId,
                StateName = dto.StateName,
                StateAbbr = dto.StateAbbr
            };
        }
    }
}