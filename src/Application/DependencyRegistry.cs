using Microsoft.Extensions.DependencyInjection;
using Ticketing.Application.Caching;
using Ticketing.Notification.Contracts;

namespace Ticketing.Application;

public static class DependencyRegistry
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddNotifications();

        services.AddMediatR(configuration =>
            configuration
                .RegisterServicesFromAssemblyContaining(typeof(DependencyRegistry))
                .AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>))
        );

        services.AddMemoryCache(options =>
        {
            options.SizeLimit = 1024;
        });

        return services;
    }

    public static IHealthChecksBuilder AddApplicationDependenciesHealthCheck(
        this IHealthChecksBuilder healthCheckBuilder) => healthCheckBuilder.AddMessageQueueHealthCheck();
}
