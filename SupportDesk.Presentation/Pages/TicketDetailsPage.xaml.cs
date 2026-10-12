using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SupportDesk.Presentation.ViewModels;
using System.ComponentModel;

namespace SupportDesk.Presentation.Pages;

public sealed partial class TicketDetailsPage : Page
{
    public TicketDetailsViewModel ViewModel { get; }

    public TicketDetailsPage(TicketDetailsViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = ViewModel;
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.IsSaveSuccess))
        {
            SaveMessageTextBlock.Foreground = new SolidColorBrush(
                ViewModel.IsSaveSuccess ? Colors.Green : Colors.Red);
        }
    }
}