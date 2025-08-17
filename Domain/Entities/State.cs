using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interface;

public class State : IState
{
    public int StateId { get; set; }

    [Required]
    public string StateName { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "StateAbbr must be exactly 2 characters.")]
    public string StateAbbr { get; set; }
}
