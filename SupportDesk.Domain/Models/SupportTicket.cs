
using SupportDesk.Domain.Enums;

namespace SupportDesk.Domain.Models;

public class SupportTicket
{
    public TicketStatus Status { get; private set; } 
    public TicketPriority Priority { get; private set; }
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public Guid CustomerId { get; private set; }
    public string? Technician { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public List<TicketComment> Comments { get; private set; } = new();


    public SupportTicket(string title,string description,Guid customerId,TicketPriority priority)
    {


        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Rubrik får inte vara tom.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Beskrivning får inte vara tom.");
        }

        if (customerId == Guid.Empty)
        {
            throw new ArgumentException("Kund-id får inte vara tomt.");
        }


        Id = Guid.NewGuid();
        Status = TicketStatus.New;
        Title = title;
        Description = description;
        CustomerId = customerId;
        Priority = priority;
        CreatedAt = DateTime.Now;
    }
    public void AssignTechnician(string technician)
    {
        if (string.IsNullOrWhiteSpace(technician))
        {
            throw new ArgumentException("Handläggare får inte vara tom.");
        }

        Technician = technician;
    }

    public void ChangeStatus(TicketStatus newStatus)
    {
        if ((newStatus == TicketStatus.InProgress ||
             newStatus == TicketStatus.Resolved) &&
            string.IsNullOrWhiteSpace(Technician))
        {
            throw new InvalidOperationException(
                "En handläggare måste vara tilldelad innan statusen kan ändras.");
        }

        Status = newStatus;
    }

    public void ChangePriority(TicketPriority newPriority)
    {
        Priority = newPriority;
    }

    public void UpdateDetails(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Rubrik får inte vara tom.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Beskrivning får inte vara tom.");
        }

        Title = title;
        Description = description;
    }

    public void AddComment(string text)
    {
        var comment = new TicketComment(text);
        Comments.Add(comment);
    }
}
