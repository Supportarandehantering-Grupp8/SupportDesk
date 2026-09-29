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
}
