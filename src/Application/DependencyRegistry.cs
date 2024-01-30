﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Application.Caching;
using Ticketing.Notification.Common;

namespace Ticketing.Application;

public static class DependencyRegistry
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddNotifications(configuration);
        
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
}
