using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Data;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using SupportDesk.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SupportDesk.Application.Customers;

namespace SupportDesk.Presentation.ViewModels;

public partial class TicketsViewModel(
    ITicketService ticketService,
    ICustomerService customerService) : ObservableObject
{
    private CancellationTokenSource? searchCancellationTokenSource;

    private int latestSearchId;

    [ObservableProperty]
    private IReadOnlyList<SupportTicket> tickets = [];
    public Dictionary<Guid, string> CustomerNames { get; } = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private TicketStatus? selectedStatus;

    [ObservableProperty]
    private string? selectedTechnician;

    [ObservableProperty]
    private TicketSortOrder selectedSortOrder = TicketSortOrder.Newest;

    public IReadOnlyList<string> Technicians { get; } =
        new[] { "Alla handläggare", "Ej tilldelad" }
        .Concat(TechnicianList.Names)
        .ToList();

    public async Task LoadTicketsAsync()
    {
        int searchId = ++latestSearchId;

        var results = await ticketService.GetBySearchAsync(
            SearchText,
            SelectedStatus,
            SelectedTechnician,
            SelectedSortOrder);

        if (searchId != latestSearchId)
        {
            return;
        }

        var customers = await customerService.GetAllAsync();

        if (searchId != latestSearchId)
        {
            return;
        }

        CustomerNames.Clear();

        foreach (var customer in customers)
        {
            CustomerNames[customer.Id] = customer.CustomerName;
        }

        Tickets = results;
    }

    public string GetCustomerName(Guid customerId)
    {
        return CustomerNames.TryGetValue(customerId, out var name)
            ? name : "Okänd kund";
    }

    partial void OnSearchTextChanged(string value)
    {
        searchCancellationTokenSource?.Cancel();

        searchCancellationTokenSource = new CancellationTokenSource();

        _ = SearchWithDelayAsync(searchCancellationTokenSource.Token);
    }

    private async Task SearchWithDelayAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(300, cancellationToken);

            await LoadTicketsAsync();
        }
        catch (OperationCanceledException)
        {
        }
    }

    partial void OnSelectedStatusChanged(TicketStatus? value)
    {
        searchCancellationTokenSource?.Cancel();

        _ = LoadTicketsAsync();
    }

    partial void OnSelectedTechnicianChanged(string? value)
    {
        searchCancellationTokenSource?.Cancel();

        _ = LoadTicketsAsync();
    }

    partial void OnSelectedSortOrderChanged(TicketSortOrder value)
    {
        searchCancellationTokenSource?.Cancel();

        _ = LoadTicketsAsync();
    }
}