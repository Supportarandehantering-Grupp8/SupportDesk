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


    // GEMENSAM DEL
    public Task UpdateTicketAsync(SupportTicket ticket)
    {
        throw new NotImplementedException();
    }


    // ALI DEL
    public async Task UpdateDetailsAsync(
        Guid id,
        string title,
        string description)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.UpdateDetails(title, description);

        await ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task AssignTechnicianAsync(
        Guid id,
        string technician)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.AssignTechnician(technician);

        await ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task ChangePriorityAsync(
        Guid id,
        TicketPriority priority)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.ChangePriority(priority);

        await ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task ChangeStatusAsync(
        Guid id,
        TicketStatus status)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.ChangeStatus(status);

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