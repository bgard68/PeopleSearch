namespace Application.DTOs
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public StateDto State { get; set; }
    }
}