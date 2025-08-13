using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
       public interface IPerson
    {
        int PersonId { get; set; }
        string FirstName { get; set; }
        string MI { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string CellNumber { get; set; }
        string Email { get; set; }
    }
}
