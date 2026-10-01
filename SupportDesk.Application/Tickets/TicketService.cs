
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;

namespace SupportDesk.Application.Tickets;

public class TicketService : ITicketService
{
    public Task CreateTicketAsync(string title, string description, Guid customerId, TicketPriority priority)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SupportTicket?> GetTicketByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SupportTicket>> GetTicketsByStatusAsync(TicketStatus status)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTicketAsync(SupportTicket ticket)
    {
        throw new NotImplementedException();
    }
}
