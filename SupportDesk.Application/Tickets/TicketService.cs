using SupportDesk.Application.Customers;
using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;

namespace SupportDesk.Application.Tickets;

public class TicketService(
    ITicketRepository ticketRepository,
    ICustomerRepository customerRepository) : ITicketService
{
    // MALIN / GEMENSAM DEL
    public async Task CreateTicketAsync(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            throw new ArgumentException(
                $"Customer with id {customerId} does not exist.",
                nameof(customerId));
        }

        SupportTicket ticket =
            new(title, description, customerId, priority);

        await ticketRepository.AddAsync(ticket);
    }


    // SEBASTIAN / GEMENSAM DEL
    public Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        throw new NotImplementedException();
    }


    // ALI DEL
    public async Task<SupportTicket?> GetTicketByIdAsync(Guid id)
    {
        return await ticketRepository.GetByIdAsync(id);
    }


    // SEBASTIAN / GEMENSAM DEL
    public Task<IReadOnlyList<SupportTicket>> GetTicketsByStatusAsync(
        TicketStatus status)
    {
        throw new NotImplementedException();
    }


    // ALI DEL
    public async Task UpdateTicketAsync(UpdateTicketRequest request)
    {
        var ticket = await GetRequiredTicketAsync(request.TicketId);

        ticket.UpdateDetails(
            request.Title,
            request.Description);

        ticket.AssignTechnician(
            request.Technician);

        ticket.ChangePriority(
            request.Priority);

        ticket.ChangeStatus(
            request.Status);

        await ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task AddCommentAsync(
        Guid id,
        string text)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.AddComment(text);

        await ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL - HJÄLPMETOD
    private async Task<SupportTicket> GetRequiredTicketAsync(Guid id)
    {
        var ticket = await ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            throw new KeyNotFoundException(
                $"Ticket med id {id} hittades inte.");
        }

        return ticket;
    }
}