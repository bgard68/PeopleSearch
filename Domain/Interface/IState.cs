namespace Domain.Interface
{
    public interface IState
    {
        int StateId { get; set; }
        string StateName { get; set; }
        string StateCode { get; set; }
    }
}