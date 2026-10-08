
using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<ITicketService, TicketService>();

        return services;
    }
}
