using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Data;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly ITicketService _ticketService;
    private readonly ICustomerService _customerService;

    public int TotalTickets { get; private set; }
    public int ActiveTickets { get; private set; }
    public int ResolvedTickets { get; private set; }

    public int TotalCustomers { get; private set; }
    public int TotalTechnicians { get; private set; }

    public ObservableCollection<CustomerTicketSummary> CustomerTicketSummaries { get; } = new();

    public ObservableCollection<TechnicianTicketSummary> TechnicianTicketSummaries { get; } = new();

    public ObservableCollection<RecentTicketSummary> RecentTickets { get; } = new();

    public HomeViewModel(
        ITicketService ticketService,
        ICustomerService customerService)
    {
        _ticketService = ticketService;
        _customerService = customerService;
    }

    public async Task LoadStatisticsAsync()
    {
        var tickets = await _ticketService.GetAllAsync();

        TotalTickets = tickets.Count;

        ActiveTickets = tickets.Count(ticket =>
            ticket.Status == TicketStatus.New ||
            ticket.Status == TicketStatus.InProgress);

        ResolvedTickets = tickets.Count(ticket =>
            ticket.Status == TicketStatus.Resolved);

        OnPropertyChanged(nameof(TotalTickets));
        OnPropertyChanged(nameof(ActiveTickets));
        OnPropertyChanged(nameof(ResolvedTickets));

        var customers = await _customerService.GetAllAsync();

        TotalCustomers = customers.Count;
        TotalTechnicians = TechnicianList.Names.Count;

        CustomerTicketSummaries.Clear();

        foreach (var customer in customers
            .Where(customer => tickets.Any(ticket =>
                ticket.CustomerId == customer.Id &&
                (ticket.Status == TicketStatus.New ||
                 ticket.Status == TicketStatus.InProgress)))
            .Take(5))
        {
            var ticketCount = tickets.Count(ticket =>
                ticket.CustomerId == customer.Id &&
                (ticket.Status == TicketStatus.New ||
                 ticket.Status == TicketStatus.InProgress));

            CustomerTicketSummaries.Add(new CustomerTicketSummary
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                TicketCount = ticketCount
            });
        }

        TechnicianTicketSummaries.Clear();

        foreach (var technician in TechnicianList.Names)
        {
            var inProgressCount = tickets.Count(ticket =>
                ticket.Technician == technician &&
                ticket.Status == TicketStatus.InProgress);

            TechnicianTicketSummaries.Add(new TechnicianTicketSummary
            {
                TechnicianName = technician,
                InProgressTickets = inProgressCount
            });
        }

        RecentTickets.Clear();

        foreach (var ticket in tickets
            .OrderByDescending(t => t.CreatedAt)
            .Take(5))
        {
            var customer = customers.FirstOrDefault(c =>
                c.Id == ticket.CustomerId);

            RecentTickets.Add(new RecentTicketSummary
            {
                CustomerName = customer?.CustomerName ?? "Okänd kund",
                Title = ticket.Title,
                CreatedAt = ticket.CreatedAt,
                TechnicianName = ticket.Technician ?? "Ej tilldelad",
                Priority = ticket.Priority.ToString(),
                Status = ticket.Status.ToString()
            });
        }

        OnPropertyChanged(nameof(TotalCustomers));
        OnPropertyChanged(nameof(TotalTechnicians));
    }
}

public class CustomerTicketSummary
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public int TicketCount { get; set; }
}

public class TechnicianTicketSummary
{
    public string TechnicianName { get; set; } = string.Empty;
    public int InProgressTickets { get; set; }
}

public class RecentTicketSummary
{
    public string CustomerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string TechnicianName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
}
