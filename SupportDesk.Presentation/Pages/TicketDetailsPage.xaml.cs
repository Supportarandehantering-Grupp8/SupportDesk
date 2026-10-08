using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation.Pages;

public sealed partial class TicketDetailsPage : Page
{
    public TicketDetailsViewModel ViewModel { get; }

    public TicketDetailsPage(TicketDetailsViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = ViewModel;
    }
}