
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Tickets;

public interface ITicketService
{
    Task CreateTicketAsync(string title, string description, Guid customerId, TicketPriority priority);
    Task<IReadOnlyList<SupportTicket>> GetAllAsync();
    Task<SupportTicket?> GetTicketByIdAsync(Guid id);
    Task<IReadOnlyList<SupportTicket>> GetTicketsByStatusAsync(TicketStatus status);
    Task UpdateTicketAsync(SupportTicket ticket);
}
