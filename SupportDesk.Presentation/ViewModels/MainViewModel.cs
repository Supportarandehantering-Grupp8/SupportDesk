using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation.ViewModels;

public partial class MainViewModel(INavigationService navigationService) : ObservableObject
{
    public INavigationService Navigation { get; } = navigationService;

    [RelayCommand]
    private void ShowHome()
    {
        Navigation.NavigateTo(AppPage.Home);
    }

    [RelayCommand]
    private void ShowCustomers()
    {
        Navigation.NavigateTo(AppPage.Customers);
    }

    [RelayCommand]
    private void ShowTickets()
    {
        Navigation.NavigateTo(AppPage.Tickets);
    }

    [RelayCommand]
    private void ShowSettings()
    {
        Navigation.NavigateTo(AppPage.Settings);
    }
}