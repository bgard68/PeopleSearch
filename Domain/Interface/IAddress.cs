using System.ComponentModel.DataAnnotations;

namespace Domain.Interface
{
    public interface IAddress
    {
        int AddressId { get; set; }
        [Required]
        string StreetAddress { get; set; }
        [Required]
        string City { get; set; }
        [Required]
        string ZipCode { get; set; }
        int StateId { get; set; } // Add this for the foreign key
    }
}