
using SupportDesk.Application.Customers;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

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

    // SEBASTIAN - All
    public async Task<IReadOnlyList<SupportTicket>> GetAllAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();

        return tickets.OrderByDescending(ticket => ticket.CreatedAt).ToList();
    }

    // SEBASTIAN - SEARH
    public async Task<IReadOnlyList<SupportTicket>> GetBySearchAsync(
        string searchText,
        TicketStatus? status,
        string? technician,
        TicketSortOrder sortOrder = TicketSortOrder.Newest)
    {
        var tickets = await ticketRepository.GetAllAsync();
        var customers = await customerRepository.GetAllAsync();

        IEnumerable<SupportTicket> filteredTickets = tickets;

        filteredTickets = ApplyStatusFilter(filteredTickets, status);
        filteredTickets = ApplyTechnicianFilter(filteredTickets, technician);
        filteredTickets = ApplySearch(filteredTickets, customers, searchText);
        filteredTickets = ApplySortOrder(filteredTickets, sortOrder);

        return filteredTickets.ToList();
    }

    // ALI DEL
    public async Task<SupportTicket?> GetTicketByIdAsync(Guid id)
    {
        return await ticketRepository.GetByIdAsync(id);
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

    // SEBASTIAN - HJÄLPMETOD
    private static IEnumerable<SupportTicket> ApplyStatusFilter(
        IEnumerable<SupportTicket> filteredTickets,
        TicketStatus? status)
    {
        if (status is null)
        {
            return filteredTickets;
        }

        return filteredTickets.Where(ticket => ticket.Status == status);
    }

    private static IEnumerable<SupportTicket> ApplyTechnicianFilter(
        IEnumerable<SupportTicket> filteredTickets,
        string? technician)
    {
        if (string.IsNullOrWhiteSpace(technician))
        {
            return filteredTickets;
        }

        return filteredTickets.Where(ticket =>
            string.Equals(
                ticket.Technician,
                technician,
                StringComparison.OrdinalIgnoreCase));
    }

    private static IEnumerable<SupportTicket> ApplySearch(
        IEnumerable<SupportTicket> filteredTickets,
        IReadOnlyList<Customer> customers,
        string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return filteredTickets;
        }

        var normalizedSearchText = searchText.Trim();

        return filteredTickets.Where(ticket =>
            ticket.Title.Contains(
                normalizedSearchText,
                StringComparison.OrdinalIgnoreCase)
            ||
            ticket.Description.Contains(
                normalizedSearchText,
                StringComparison.OrdinalIgnoreCase)
            ||
            customers.Any(customer =>
                customer.Id == ticket.CustomerId &&
                customer.CustomerName.Contains(
                    normalizedSearchText,
                    StringComparison.OrdinalIgnoreCase)));
    }

    private static IEnumerable<SupportTicket> ApplySortOrder(
        IEnumerable<SupportTicket> filteredTickets,
        TicketSortOrder sortOrder)
    {
        if (sortOrder == TicketSortOrder.Newest)
        {
            return filteredTickets.OrderByDescending(
                ticket => ticket.CreatedAt);
        }

        return filteredTickets.OrderBy(
            ticket => ticket.CreatedAt);
    }
}