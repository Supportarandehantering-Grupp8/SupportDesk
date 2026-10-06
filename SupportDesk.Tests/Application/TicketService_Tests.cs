using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Application;

public class TicketService_Tests
{
    [Fact]
    public async Task GetTicketByIdAsync_ShouldReturnTicket_WhenTicketExists()
    {
        var ticket = CreateTicket();

        var ticketRepository = new FakeTicketRepository();
        ticketRepository.Tickets.Add(ticket);

        var customerRepository = new FakeCustomerRepository();

        var service = new TicketService(
            ticketRepository,
            customerRepository);

        var result = await service.GetTicketByIdAsync(ticket.Id);

        Assert.NotNull(result);
        Assert.Equal(ticket.Id, result.Id);
    }

    [Fact]
    public async Task UpdateTicketAsync_ShouldUpdateTicketAndSave()
    {
        var ticket = CreateTicket();

        var ticketRepository = new FakeTicketRepository();
        ticketRepository.Tickets.Add(ticket);

        var customerRepository = new FakeCustomerRepository();

        var service = new TicketService(
            ticketRepository,
            customerRepository);

        var request = new UpdateTicketRequest
        {
            TicketId = ticket.Id,
            Title = "Ny titel",
            Description = "Ny beskrivning",
            Technician = "Ali",
            Priority = TicketPriority.High,
            Status = TicketStatus.InProgress
        };

        await service.UpdateTicketAsync(request);

        Assert.Equal("Ny titel", ticket.Title);
        Assert.Equal("Ny beskrivning", ticket.Description);
        Assert.Equal("Ali", ticket.Technician);
        Assert.Equal(TicketPriority.High, ticket.Priority);
        Assert.Equal(TicketStatus.InProgress, ticket.Status);

        Assert.True(ticketRepository.UpdateCalled);
    }

    [Fact]
    public async Task AddCommentAsync_ShouldAddCommentAndSave()
    {
        var ticket = CreateTicket();

        var ticketRepository = new FakeTicketRepository();
        ticketRepository.Tickets.Add(ticket);

        var customerRepository = new FakeCustomerRepository();

        var service = new TicketService(
            ticketRepository,
            customerRepository);

        await service.AddCommentAsync(
            ticket.Id,
            "Problemet är undersökt.");

        Assert.Single(ticket.Comments);
        Assert.Equal(
            "Problemet är undersökt.",
            ticket.Comments[0].Text);

        Assert.True(ticketRepository.UpdateCalled);
    }

    [Fact]
    public async Task AddCommentAsync_ShouldThrow_WhenTicketDoesNotExist()
    {
        var ticketRepository = new FakeTicketRepository();
        var customerRepository = new FakeCustomerRepository();

        var service = new TicketService(
            ticketRepository,
            customerRepository);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => service.AddCommentAsync(
                Guid.NewGuid(),
                "Kommentar"));
    }

    private static SupportTicket CreateTicket()
    {
        return new SupportTicket(
            "Test title",
            "Test description",
            Guid.NewGuid(),
            TicketPriority.Normal);
    }


    private class FakeTicketRepository : ITicketRepository
    {
        public List<SupportTicket> Tickets { get; } = [];

        public bool UpdateCalled { get; private set; }


        public Task AddAsync(SupportTicket ticket)
        {
            Tickets.Add(ticket);

            return Task.CompletedTask;
        }


        public Task<IReadOnlyList<SupportTicket>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyList<SupportTicket>>(
                Tickets);
        }


        public Task<SupportTicket?> GetByIdAsync(Guid id)
        {
            var ticket = Tickets.FirstOrDefault(
                ticket => ticket.Id == id);

            return Task.FromResult(ticket);
        }


        public Task<IReadOnlyList<SupportTicket>> GetByStatusAsync(
            TicketStatus status)
        {
            var tickets = Tickets
                .Where(ticket => ticket.Status == status)
                .ToList();

            return Task.FromResult<IReadOnlyList<SupportTicket>>(
                tickets);
        }


        public Task UpdateAsync(SupportTicket ticket)
        {
            UpdateCalled = true;

            return Task.CompletedTask;
        }
    }


    private class FakeCustomerRepository : ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync()
        {
            return Task.FromResult(new List<Customer>());
        }


        public Task AddAsync(Customer customer)
        {
            return Task.CompletedTask;
        }


        public Task<bool> UpdateAsync(Customer customer)
        {
            return Task.FromResult(true);
        }


        public Task<Customer?> GetByIdAsync(Guid id)
        {
            return Task.FromResult<Customer?>(null);
        }
    }
}