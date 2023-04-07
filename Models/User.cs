namespace AvtotestMVC.Models;

public class User
{
    public string Id { get; set; }
    public string Name {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public string? PhotoPath {get; set;}
    public List<TicketResult> TicketResults { get; set;}
    public List<Ticket> Tickets { get; set; }

    public User()
    {
        TicketResults = new List<TicketResult>();
    }
}
