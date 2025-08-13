namespace Domain.Interface
{
    public interface IAddress
    {
        int AddressId { get; set; }
        string StreetAddress { get; set; }
        int CityId { get; set; }
        int ZipCodeId { get; set; }
    }
}