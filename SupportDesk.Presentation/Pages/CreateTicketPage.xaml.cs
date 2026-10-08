
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
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

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        await ViewModel.LoadCustomersAsync();
    }
}
