using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SupportDesk.Application;
using SupportDesk.Infrastructure;
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

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            _services = services.BuildServiceProvider();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = _services.GetRequiredService<MainWindow>();
            _window.Activate();
        }
    }
}
