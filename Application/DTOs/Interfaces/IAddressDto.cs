namespace Application.DTOs.Interfaces
{
    public interface IAddressDto
    {
        int AddressId { get; set; }
        string StreetAddress { get; set; }
        string City { get; set; }
        int StateId { get; set; }
        string ZipCode { get; set; }
        StateDto State { get; set; }
    }
}