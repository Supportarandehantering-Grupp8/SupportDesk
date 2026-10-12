using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Pages;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        AppNavigation.BackRequested += AppNavigation_BackRequested;

        ViewModel.Navigation.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == "CurrentPage")
            {
                UpdateNavigation();
            }
        };

        UpdateNavigation();
    }

    private void UpdateNavigation()
    {
        // Aktivera eller inaktivera Tillbaka-knappen
        AppNavigation.IsBackEnabled = ViewModel.Navigation.CanGoBack;

        // Hämta sidan som visas
        var currentPage = ViewModel.Navigation.CurrentPage;

        // Markera rätt menyknapp
        switch (currentPage)
        {
            case HomePage:
                AppNavigation.SelectedItem = HomeNavigationItem;
                break;

            case CustomersPage:
                AppNavigation.SelectedItem = CustomersNavigationItem;
                break;

            case TicketsPage:
                AppNavigation.SelectedItem = TicketsNavigationItem;
                break;

            case CreateTicketPage:
            case TicketDetailsPage:
                AppNavigation.SelectedItem = TicketsNavigationItem;
                break;

            case SettingsPage:
                AppNavigation.SelectedItem = AppNavigation.SettingsItem;
                break;
        }
    }

    private void AppNavigation_BackRequested(
        NavigationView sender,
        NavigationViewBackRequestedEventArgs args)
    {
        if (ViewModel.Navigation.CanGoBack)
        {
            ViewModel.Navigation.GoBack();
        }
    }

    private void AppNavigation_ItemInvoked(
        NavigationView sender,
        NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            ViewModel.ShowSettingsCommand.Execute(null);
            return;
        }

        if (args.InvokedItemContainer?.Tag is string tag)
        {
            switch (tag)
            {
                case "home":
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;

                case "customers":
                    ViewModel.ShowCustomersCommand.Execute(null);
                    break;

                case "tickets":
                    ViewModel.ShowTicketsCommand.Execute(null);
                    break;
            }
        }
    }
}