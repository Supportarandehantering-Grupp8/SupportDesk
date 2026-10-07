using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;

namespace SupportDesk.Tests.Overview;

public class TicketOverviewTestData
{
    public TicketService TicketService { get; private set; } = null!;
    public static async Task<TicketOverviewTestData> CreateAsync()
    {
        var ticketRepository = new FakeTicketRepository();
        var customerRepository = new FakeCustomerRepository();

        var ticketService = new TicketService(
            ticketRepository,
            customerRepository);

        var sebastian = new Customer(
            "Sebastian",
            "sebastian@test.se");

        var malin = new Customer(
            "Malin",
            "malin@test.se");

        var ahli = new Customer(
            "Ahli",
            "ahli@test.se");

        var scott = new Customer(
            "Scott",
            "scott@test.se");

        await customerRepository.AddAsync(sebastian);
        await customerRepository.AddAsync(malin);
        await customerRepository.AddAsync(ahli);
        await customerRepository.AddAsync(scott);

        var printerTicket = new SupportTicket(
            "Skrivaren",
            "Kan inte skriva ut dokument",
            sebastian.Id,
            TicketPriority.Normal);

        await ticketRepository.AddAsync(printerTicket);

        var networkTicket = new SupportTicket(
            "Problem med nätverket",
            "Kunden saknar internetanslutning",
            malin.Id,
            TicketPriority.High);

        networkTicket.AssignTechnician("Alex");
        networkTicket.ChangeStatus(TicketStatus.Resolved);

        await ticketRepository.AddAsync(networkTicket);
        
        var computerTiket = new SupportTicket(
            "Datorn startar inte",
            "Datorn reagerar inte när kunden försöker starta den",
            ahli.Id,
            TicketPriority.High);

        await ticketRepository.AddAsync(computerTiket);

        var mailTicket = new SupportTicket(
            "E-post fungerar inte",
            "Kunden kan inte skicka eller ta emot e-post",
            ahli.Id,
            TicketPriority.Normal);

        mailTicket.AssignTechnician("Peter");
        mailTicket.ChangeStatus(TicketStatus.InProgress);

        await ticketRepository.AddAsync(mailTicket);

        return new TicketOverviewTestData
        {
            TicketService = ticketService
        };
    }


}