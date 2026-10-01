namespace SupportDesk.Application.Models;

public class TicketComment
{
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public TicketComment(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Kommentar får inte vara tom.");
        }

        Text = text.Trim();
        CreatedAt = DateTime.Now;
    }
}