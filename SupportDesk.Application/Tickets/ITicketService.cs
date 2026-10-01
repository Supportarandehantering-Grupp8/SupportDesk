
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;

namespace SupportDesk.Application.Tickets;

public interface ITicketService
{
    Task CreateTicketAsync(string title, string description, Guid customerId, TicketPriority priority);
    Task<IReadOnlyList<SupportTicket>> GetAllAsync();
    Task<SupportTicket?> GetTicketByIdAsync(Guid id);
    Task<IReadOnlyList<SupportTicket>> GetTicketsByStatusAsync(TicketStatus status);
    Task UpdateTicketAsync(SupportTicket ticket);

    Task UpdateDetailsAsync(Guid id, string title, string description);

    Task AssignTechnicianAsync(Guid id, string technician);

    Task ChangePriorityAsync(Guid id, TicketPriority priority);

    Task ChangeStatusAsync(Guid id, TicketStatus status);

    Task AddCommentAsync(Guid id, string text);
}
