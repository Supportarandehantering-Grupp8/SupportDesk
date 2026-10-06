
using NSubstitute;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Application;

public class TicketService_Tests
{
    [Fact]
    public async Task CreateTicketAsync_Should_CreateTicketWithCorrectValues()
    {
        var ticketRepository = Substitute.For<ITicketRepository>();
        var customerRepository = Substitute.For<ICustomerRepository>();

        var ticketService = new TicketService(ticketRepository, customerRepository);

        var customer = new Customer("Kund Kundsson", "kund@email.com");

        customerRepository.GetByIdAsync(customer.Id).Returns(customer);

        string title = "Kan inte logga in";
        string description = "Det går inte att logga in via webbsidan";
        Guid customerId = customer.Id;
        TicketPriority priority = TicketPriority.High;

        await ticketService.CreateTicketAsync(title, description, customerId, priority);

        await ticketRepository.Received(1).AddAsync(Arg.Is<SupportTicket>(t => 
            t.Title == title &&
            t.Description == description &&
            t.CustomerId == customerId &&
            t.Priority == priority &&
            t.Id != Guid.Empty &&
            t.CreatedAt != default
        ));
    }
}
