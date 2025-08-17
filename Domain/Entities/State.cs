using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interface;

namespace Domain.Entities
{
    public class State
    {
        public int StateId { get; set; }

        public string StateName { get; set; }
        public string StateAbbr { get; set; }
    }
}
