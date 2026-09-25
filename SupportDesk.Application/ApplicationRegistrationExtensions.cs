
using Microsoft.Extensions.DependencyInjection;

namespace SupportDesk.Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
