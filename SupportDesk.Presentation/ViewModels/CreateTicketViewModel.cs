
using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Domain.Enums;
using System.Collections.ObjectModel;
using SupportDesk.Domain.Models;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation.ViewModels;

public partial class CreateTicketViewModel(
    ITicketService ticketService, 
    ICustomerService customerService,
    INavigationService navigationService) : ObservableObject
{
    [ObservableProperty]
    public partial string Title {  get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial ObservableCollection<Customer> Customers { get; set; } = [];

    [ObservableProperty]
    public partial Customer? SelectedCustomer { get; set; }

    [ObservableProperty]
    public partial TicketPriority Priority { get; set; } = TicketPriority.Normal;

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SuccessMessage { get; set; } = string.Empty;

    [RelayCommand]
    private async Task CreateTicket()
    {
        ErrorMessage = string.Empty;

        try
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                    ErrorMessage = "Du måste ange en titel";
                    return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                ErrorMessage = "Du måste lägga till en beskrivning";
                return;
            }

            if (SelectedCustomer is null)
            {
                ErrorMessage = "Du måste välja en kund";
                return;
            }

            await ticketService.CreateTicketAsync(Title, Description, SelectedCustomer.Id, Priority);

            SuccessMessage = $"Ärende '{Title}' har lagts till.";

            Title = string.Empty;
            Description = string.Empty;
            SelectedCustomer = null;
            Priority = TicketPriority.Normal;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        navigationService.NavigateTo(AppPage.Tickets);
    }

    public void ClearSuccessMessage()
    {
        SuccessMessage = string.Empty;
    }

    public async Task LoadCustomersAsync()
    {
        var customers = await customerService.GetAllAsync();

        Customers = new ObservableCollection<Customer>(customers);
    }
}
