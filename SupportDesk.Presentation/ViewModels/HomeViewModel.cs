using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Data;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
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

        OnPropertyChanged(nameof(TotalCustomers));
        OnPropertyChanged(nameof(TotalTechnicians));
    }
}