using SupportDesk.Domain.Models;

namespace SupportDesk.Application.Tickets;

public interface ITicketRepository
{
    void Add(SupportTicket ticket);

    IReadOnlyList<SupportTicket> GetAll();

    SupportTicket? GetById(Guid id);

    void Update(SupportTicket ticket);
}