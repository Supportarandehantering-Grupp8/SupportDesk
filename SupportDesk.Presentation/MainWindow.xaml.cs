using Microsoft.UI.Xaml;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }
    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;
        
        InitializeComponent();
    }

    private void AppNavigation_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
    {
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
