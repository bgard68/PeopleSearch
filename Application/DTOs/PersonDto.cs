namespace Application.DTOs
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? CellNumber { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public HomeAddressDto Address { get; set; }
    }
}