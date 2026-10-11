using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Presentation.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupportDesk.Presentation.Navigation;

public partial class NavigationService(IServiceProvider services)
    : ObservableObject, INavigationService
{
    private readonly Stack<Page> _history = new();

    [ObservableProperty]
    public partial Page? CurrentPage { get; private set; }

    public bool CanGoBack => _history.Count > 0;

    public void NavigateTo(AppPage page)
    {
        Page newPage = page switch
        {
            AppPage.Home => services.GetRequiredService<HomePage>(),
            AppPage.Customers => services.GetRequiredService<CustomersPage>(),
            AppPage.Tickets => services.GetRequiredService<TicketsPage>(),
            AppPage.CreateTicket => services.GetRequiredService<CreateTicketPage>(),
            AppPage.TicketDetails => services.GetRequiredService<TicketDetailsPage>(),
            AppPage.Settings => services.GetRequiredService<SettingsPage>(),
            _ => throw new ArgumentOutOfRangeException(nameof(page))
        };

        if (CurrentPage != null)
            _history.Push(CurrentPage);

        CurrentPage = newPage;
    }

    public async Task NavigateToTicketDetailsAsync(Guid ticketId)
    {
        var page = services.GetRequiredService<TicketDetailsPage>();

        if (CurrentPage != null)
            _history.Push(CurrentPage);

        CurrentPage = page;

        await page.ViewModel.LoadTicketAsync(ticketId);
    }

    public void GoBack()
    {
        if (!CanGoBack)
            return;

        CurrentPage = _history.Pop();
    }
}