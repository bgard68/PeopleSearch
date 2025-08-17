using System.ComponentModel.DataAnnotations;
using Domain.Interface;

namespace Domain.Entities
{
    public class Address : IAddress
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "ZipCode must be exactly 5 digits.")]
        public string ZipCode { get; set; }

        public State State { get; set; } // Navigation property
    }
}
