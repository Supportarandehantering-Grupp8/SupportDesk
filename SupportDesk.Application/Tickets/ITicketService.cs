
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Tickets;

public interface ITicketService
{
    Task CreateTicketAsync(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority);

    Task<IReadOnlyList<SupportTicket>> GetAllAsync();

    Task<SupportTicket?> GetTicketByIdAsync(Guid id);

    Task UpdateTicketAsync(UpdateTicketRequest request);

    Task AddCommentAsync(Guid id, string text);

    Task<IReadOnlyList<SupportTicket>> GetBySearchAsync(
        string searchText,
        TicketStatus? status,
        string? technician,
        TicketSortOrder sortOrder = TicketSortOrder.Newest);
}