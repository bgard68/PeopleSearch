using Domain.Interface;
public class CityZipCode{
    public int CityId { get; set; }
    public int ZipCodeId { get; set; }
    public City City { get; set; } = null!;
    public ZipCode ZipCode { get; set; } = null!;
}