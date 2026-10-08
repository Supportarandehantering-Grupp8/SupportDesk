
using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Tickets;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ITicketService, TicketService>();

        return services;
    }
}
