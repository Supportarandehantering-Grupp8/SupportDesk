using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Models;
using SupportDesk.Domain.Enums;

namespace SupportDesk.Tests.Overview;

public class FakeTicketRepository : ITicketRepository
{
    private readonly List<SupportTicket> _tickets = new();

    public Task AddAsync(SupportTicket ticket)
    {
        _tickets.Add(ticket);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<SupportTicket>>(_tickets);
    }

    public Task<IReadOnlyList<SupportTicket>> GetByStatusAsync(TicketStatus status)
    {
        var tickets = _tickets.Where(ticket => ticket.Status == status).ToList();

        return Task.FromResult<IReadOnlyList<SupportTicket>>(tickets);
    }

    public Task<SupportTicket?> GetByIdAsync(Guid id)
    {
        var ticket = _tickets.FirstOrDefault(ticket => ticket.Id == id);

        return Task.FromResult(ticket);
    }

    public Task UpdateAsync(SupportTicket ticket)
    {
        var index = _tickets.FindIndex(existingTicket => existingTicket.Id == ticket.Id);

        if (index >= 0)
        {
            _tickets[index] = ticket;
        }

        return Task.CompletedTask;
    }
}

