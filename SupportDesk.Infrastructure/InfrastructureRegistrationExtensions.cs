
using Microsoft.Extensions.DependencyInjection;
using SupportDesk.Application.Customers;
using SupportDesk.Infrastructure.Customers;
using System;
using System.IO;
using SupportDesk.Application.Tickets;
using SupportDesk.Infrastructure.Data;
using SupportDesk.Infrastructure.Tickets;

namespace SupportDesk.Infrastructure;

public static class InfrastructureRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        var filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "customers.json");

        services.AddSingleton<ICustomerRepository>(
            new JsonCustomerRepository(filePath));
        services.AddSingleton<ITicketRepository>(new JsonTicketRepository(DataPaths.Tickets));

        return services;
    }
}
