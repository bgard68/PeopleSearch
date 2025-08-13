using Domain.Interface;
public class State : IState
{
    public int StateId { get; set; }
    public string StateName { get; set; } = null!;
    public string StateCode { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = new List<City>();
}