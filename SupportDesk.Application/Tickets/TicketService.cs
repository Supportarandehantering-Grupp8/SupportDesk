using SupportDesk.Application.Enums;
using SupportDesk.Application.Models;

namespace SupportDesk.Application.Tickets;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }


    // MALIN / GEMENSAM DEL
    public Task CreateTicketAsync(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority)
    {
        throw new NotImplementedException();
    }


    // SEBASTIAN / GEMENSAM DEL
    public Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        throw new NotImplementedException();
    }


    // ALI DEL
    public async Task<SupportTicket?> GetTicketByIdAsync(Guid id)
    {
        return await _ticketRepository.GetByIdAsync(id);
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

        await _ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task AssignTechnicianAsync(
        Guid id,
        string technician)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.AssignTechnician(technician);

        await _ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task ChangePriorityAsync(
        Guid id,
        TicketPriority priority)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.ChangePriority(priority);

        await _ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task ChangeStatusAsync(
        Guid id,
        TicketStatus status)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.ChangeStatus(status);

        await _ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL
    public async Task AddCommentAsync(
        Guid id,
        string text)
    {
        var ticket = await GetRequiredTicketAsync(id);

        ticket.AddComment(text);

        await _ticketRepository.UpdateAsync(ticket);
    }


    // ALI DEL - HJÄLPMETOD
    private async Task<SupportTicket> GetRequiredTicketAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket == null)
        {
            throw new KeyNotFoundException(
                $"Ticket med id {id} hittades inte.");
        }

        return ticket;
    }
}