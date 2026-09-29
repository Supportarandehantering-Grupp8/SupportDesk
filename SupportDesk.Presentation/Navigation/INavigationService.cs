using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace SupportDesk.Presentation.Navigation;

public interface INavigationService : INotifyPropertyChanged
{
    Page? CurrentPage { get; }
    void NavigateTo(AppPage page);
}
