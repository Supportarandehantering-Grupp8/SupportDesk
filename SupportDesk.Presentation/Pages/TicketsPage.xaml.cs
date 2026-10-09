using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Application.Tickets;
using SupportDesk.Presentation.ViewModels;
using SupportDesk.Domain.Enums;
using SupportDesk.Presentation.Navigation;

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

        ViewModel.SelectedTechnician = comboBox.SelectedItem as string;
    }

    private void ClearTechnicianFilter_Click(
        object sender,
        RoutedEventArgs e)
    {
        TechnicianComboBox.SelectedItem = null;
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
}
