using Domain.Interface;

public class CityName : ICityName
{
    public int CityNameId { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = new List<City>();
}