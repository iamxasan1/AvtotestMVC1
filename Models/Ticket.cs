namespace AvtotestMVC.Models;

public class Ticket: TicketResult
{
    public int QuestionCount { get; set; } = 10;
    public int TicketIndex { get; set; }
    public int StartIndex { get; set; }
    public List<QuestionAnswer> Answers { get; set; }
}

public class QuestionAnswer
{
    public int QuestionIndex { get; set;}
    public int ChoiceIndex { get; set;}
}
