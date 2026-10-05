
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Tickets;

public class TicketService(ITicketRepository ticketRepository, ICustomerRepository customerRepository) : ITicketService
{
    public async Task CreateTicketAsync(string title, string description, Guid customerId, TicketPriority priority)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
            throw new ArgumentException($"Customer with id {customerId} does not exist.", nameof(customerId));

        SupportTicket ticket = new(title, description, customerId, priority);
        await ticketRepository.AddAsync(ticket);
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
