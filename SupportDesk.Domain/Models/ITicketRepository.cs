using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Tickets;

public interface ITicketRepository
{
    Task AddAsync(SupportTicket ticket);
    Task<IReadOnlyList<SupportTicket>> GetAllAsync();
    Task<SupportTicket?> GetByIdAsync(Guid id);
    Task UpdateAsync(SupportTicket ticket);
}