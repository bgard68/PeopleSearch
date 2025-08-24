using Application.DTOs.Interfaces;

namespace Application.DTOs
{
    public class HomeAddressDto : IAddressDto
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public StateDto State { get; set; }
    }
}