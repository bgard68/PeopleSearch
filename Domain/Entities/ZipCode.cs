using Domain.Interface;
public class ZipCode : IZipCode
{
    public int ZipCodeId { get; set; }
    public string Zip { get; set; } = null!;
    public ICollection<CityZipCode> CityZipCodes { get; set; } = new List<CityZipCode>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}