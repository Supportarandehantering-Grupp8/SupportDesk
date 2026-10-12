using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Application.Tickets;
using SupportDesk.Infrastructure.Customers;
using SupportDesk.Infrastructure.Data;
using SupportDesk.Infrastructure.Tickets;

namespace SupportDesk.Infrastructure;

public static class InfrastructureRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository>(
            new JsonCustomerRepository(DataPaths.Customers));

        services.AddSingleton<ITicketRepository>(
            new JsonTicketRepository(DataPaths.Tickets));

        return services;
    }
}