namespace AvtotestMVC.Models;

public class TicketResult 
{
    public int CorrectCount { get; set; }
    public int QuestionCount { get; set; }
    public int TicketIndex{ get; set; }
    public DateTime GetDateTime() { return DateTime.Now; }
    public bool isSolved { get; set; }
    public bool[] isCorrect { get; set; }
    public int correctCount { get; set; }
    public TicketResult()

    {
        bool isSolved = false;
        bool[] isCorrect = new bool[10];
        correctCount = 0;
        foreach (var i in isCorrect)
        {
            isSolved = true;
            correctCount++;
        }
    }
}
