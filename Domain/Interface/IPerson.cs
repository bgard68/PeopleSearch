using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
       public interface IPerson
    {
        
        int PersonId { get; set; }
        [Required]
        string FirstName { get; set; }
        string MI { get; set; }
        [Required]
        string LastName { get; set; }
        [Required]
        string PhoneNumber { get; set; }
        string? CellNumber { get; set; }
        [Required]
        string Email { get; set; }
    }
}
