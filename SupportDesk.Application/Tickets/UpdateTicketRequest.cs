using SupportDesk.Domain.Enums;

namespace SupportDesk.Application.Tickets;

public class UpdateTicketRequest
{
    public Guid TicketId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Technician { get; set; } = string.Empty;

    public TicketPriority Priority { get; set; }

    public TicketStatus Status { get; set; }
}