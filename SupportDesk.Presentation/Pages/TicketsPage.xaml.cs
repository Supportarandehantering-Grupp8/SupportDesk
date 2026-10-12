using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Domain.Models;
using SupportDesk.Presentation.ViewModels;
using SupportDesk.Domain.Enums;
using SupportDesk.Presentation.Navigation;
using System;

namespace SupportDesk.Presentation.Pages;

public sealed partial class TicketsPage : Page
{
    public TicketsViewModel ViewModel { get; }

    private readonly NavigationService navigationService;

    public TicketsPage(
        TicketsViewModel viewModel,
        NavigationService navigationService)
    {
        InitializeComponent();

        ViewModel = viewModel;
        this.navigationService = navigationService;

        Loaded += TicketsPage_Loaded;
    }

    private async void TicketsPage_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadTicketsAsync();
    }

    private void StatusComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (ViewModel is null)
        {
            return;
        }

        if (sender is not ComboBox comboBox)
        {
            return;
        }

        ViewModel.SelectedStatus = comboBox.SelectedIndex switch
        {
            1 => TicketStatus.New,
            2 => TicketStatus.InProgress,
            3 => TicketStatus.Resolved,
            _ => null
        };
    }

    private void TechnicianComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (ViewModel is null)
        {
            return;
        }

        if (sender is not ComboBox comboBox)
        {
            return;
        }

        string? selectedTechnician = comboBox.SelectedItem as string;

        ViewModel.SelectedTechnician = selectedTechnician == "Alla handläggare" ? null : selectedTechnician;
    }

    private void ClearAllFilters_Click(
        object sender,
        RoutedEventArgs e)
    {
        ViewModel.SearchText = string.Empty;

        StatusComboBox.SelectedItem = null;
        TechnicianComboBox.SelectedItem = null;
        SortComboBox.SelectedIndex = 0;

        ViewModel.SelectedSortOrder = TicketSortOrder.Newest;
    }

    private void SortComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (ViewModel is null)
        {
            return;
        }

        if (sender is not ComboBox comboBox)
        {
            return;
        }

        ViewModel.SelectedSortOrder = comboBox.SelectedIndex switch
        {
            1 => TicketSortOrder.Oldest,
            _ => TicketSortOrder.Newest
        };
    }

    private void CreateTicketButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        navigationService.NavigateTo(AppPage.CreateTicket);
    }
    private void CustomerName_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is TextBlock textBlock &&
            textBlock.Tag is Guid customerId)
        {
            textBlock.Text = ViewModel.GetCustomerName(customerId);
        }
    }

    private async void TicketsListView_SelectionChanged(
    object sender,
    SelectionChangedEventArgs e)
    {
        if (sender is ListView listView &&
            listView.SelectedItem is SupportTicket ticket)
        {
            await navigationService.NavigateToTicketDetailsAsync(ticket.Id);
        }
    }
}
