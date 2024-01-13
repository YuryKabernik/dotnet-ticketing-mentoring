using Microsoft.Extensions.DependencyInjection;

namespace Ticketing.Application;

public static class DependencyRegistry
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyRegistry)));

        return services;
    }
}
