using SupportDesk.Application.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();

        return services;
    }
}
