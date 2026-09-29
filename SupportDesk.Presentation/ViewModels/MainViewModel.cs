
using CommunityToolkit.Mvvm.ComponentModel;
using SupportDesk.Presentation.Navigation;

namespace SupportDesk.Presentation.ViewModels;

public partial class MainViewModel(INavigationService navigationService) : ObservableObject
{
    public INavigationService Navigation { get; } = navigationService;
}
