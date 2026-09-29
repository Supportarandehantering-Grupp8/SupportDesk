
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Pages;
using System;

namespace SupportDesk.Presentation.Navigation;

public partial class NavigationService(IServiceProvider services) : ObservableObject, INavigationService
{
    [ObservableProperty]
    public partial Page? CurrentPage { get; private set; }

    public void NavigateTo(AppPage page)
    {
        CurrentPage = page switch
        {
            AppPage.Home => services.GetRequiredService<HomePage>(),
            AppPage.Customers => services.GetRequiredService<CustomersPage>(),
            AppPage.Tickets => services.GetRequiredService<TicketsPage>(),
            AppPage.CreateTicket => services.GetRequiredService<CreateTicketPage>(),
            AppPage.TicketDetails => services.GetRequiredService<TicketDetailsPage>(),
            _ => throw new ArgumentOutOfRangeException(nameof(page))
        };
    }
}
