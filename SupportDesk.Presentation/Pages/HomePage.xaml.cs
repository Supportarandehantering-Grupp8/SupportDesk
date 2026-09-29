using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.ViewModels;

namespace SupportDesk.Presentation.Pages;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel { get; }
    public HomePage(HomeViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
