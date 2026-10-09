using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Tickets;
using SupportDesk.Infrastructure.Data;
using SupportDesk.Infrastructure.Tickets;
using SupportDesk.Application.Customers;
using SupportDesk.Infrastructure.Customers;

namespace SupportDesk.Infrastructure;

public static class InfrastructureRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITicketRepository>(new JsonTicketRepository(DataPaths.Tickets));

        services.AddSingleton<ICustomerRepository>(new JsonCustomerRepository(DataPaths.Customers));

        return services;
    }
}
