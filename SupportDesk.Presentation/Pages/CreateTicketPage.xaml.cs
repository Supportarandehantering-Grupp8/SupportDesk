
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportDesk.Domain.Enums;
using SupportDesk.Presentation.ViewModels;


namespace SupportDesk.Presentation.Pages;

public sealed partial class CreateTicketPage : Page
{
    public CreateTicketViewModel ViewModel { get; }
    public CreateTicketPage(CreateTicketViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private async void CreateTicketPageLoaded(
        object sender,
        Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.LoadCustomersAsync();
    }

    private void Priority_Radiobutton_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (sender is not RadioButton radiobutton)
            return;

        string? tag = radiobutton.Tag?.ToString();

        switch (tag)
        {
            case "High":
                ViewModel.Priority = TicketPriority.High;
                break;
            case "Normal":
                ViewModel.Priority = TicketPriority.Normal;
                break;
            case "Low":
                ViewModel.Priority = TicketPriority.Low;
                break;
        }
    }
}
