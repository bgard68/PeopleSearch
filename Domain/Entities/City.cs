using Domain.Interface;
public class City:ICity
{
    public int CityId { get; set; }
    public int CityNameId { get; set; }
    public int StateId { get; set; }
    public CityName CityName { get; set; } = null!;
    public State State { get; set; } = null!;
    public ICollection<CityZipCode> CityZipCodes { get; set; } = new List<CityZipCode>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}