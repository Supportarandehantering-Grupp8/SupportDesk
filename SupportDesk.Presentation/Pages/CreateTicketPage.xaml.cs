
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SupportDesk.Domain.Enums;
using SupportDesk.Presentation.ViewModels;
using System.ComponentModel;


namespace SupportDesk.Presentation.Pages;

public sealed partial class CreateTicketPage : Page
{
    public CreateTicketViewModel ViewModel { get; }
    public CreateTicketPage(CreateTicketViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
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

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.Priority) && ViewModel.Priority == TicketPriority.Normal)
        {
            Normal_Priority_RadioButton.IsChecked = true;
        }
    }

    private void Input_GotFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.ClearSuccessMessage();
    }
}
