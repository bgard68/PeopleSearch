using Domain.Entities;
using Domain.Interface;
public class Address : IAddress
{
    public int AddressId { get; set; }
    public string StreetAddress { get; set; } = null!;
    public int CityId { get; set; }
    public int ZipCodeId { get; set; }
    public City City { get; set; } = null!;
    public ZipCode ZipCode { get; set; } = null!;
    public ICollection<Person> People { get; set; } = new List<Person>();
}