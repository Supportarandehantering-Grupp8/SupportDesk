using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation.Pages;

public sealed partial class CustomersPage : Page
{
    public CustomersViewModel ViewModel { get; }

    public CustomersPage(CustomersViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        DataContext = ViewModel;

        Loaded += CustomersPage_Loaded;
    }

    private async void CustomersPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.LoadCustomersAsync();
    }


}