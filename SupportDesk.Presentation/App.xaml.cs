using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportDesk.Application;
using SupportDesk.Infrastructure;
using SupportDesk.Presentation.Navigation;
using SupportDesk.Presentation.Pages;
using SupportDesk.Presentation.ViewModels;
using System;

namespace SupportDesk.Presentation
{
    public partial class App : Microsoft.UI.Xaml.Application
    {
        private readonly IServiceProvider _services;
        private Window? _window;
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            services.AddInfrastructure();
            services.AddApplication();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            services.AddTransient<HomeViewModel>();
            services.AddTransient<HomePage>();

            services.AddTransient<CustomersViewModel>();
            services.AddTransient<CustomersPage>();

            services.AddTransient<TicketsPage>();

            services.AddTransient<CreateTicketViewModel>();
            services.AddTransient<CreateTicketPage>();

            services.AddTransient<TicketDetailsViewModel>();

            services.AddTransient<TicketDetailsPage>();

            _services = services.BuildServiceProvider();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = _services.GetRequiredService<MainWindow>();

            var navigation = _services.GetRequiredService<INavigationService>();
            navigation.NavigateTo(AppPage.Home);
       
            _window.Activate();
        }
    }
}
