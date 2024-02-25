using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ticketing.Notification.Contracts.Producers;
using Ticketing.Notification.Contracts.Producers.Interfaces;
using Ticketing.Notification.Contracts.Settings;

namespace Ticketing.Notification.Contracts;

public static class DependencyRegister
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        var assembly = Assembly.GetEntryAssembly();

        // More info here: https://andrewlock.net/simplifying-dependency-injection-for-iconfigureoptions-with-the-configureoptions-helper/
        services.ConfigureOptions<MessageBrokerConfigureOptions>();

        services.AddMassTransit(busRegister =>
        {
            busRegister.SetKebabCaseEndpointNameFormatter();
            busRegister.AddConsumers(assembly);

            busRegister.UsingRabbitMq((busContext, mqConfigurator) =>
            {
                MessageBrokerSettings mq = busContext.GetRequiredService<IOptions<MessageBrokerSettings>>().Value;

                mqConfigurator.Host(new Uri(mq.ConnectionString));
                mqConfigurator.ConfigureEndpoints(busContext);
            });
        });

        services.AddScoped<IMessageProducer, MessageProducer>();

        return services;
    }

    public static IHealthChecksBuilder AddMessageQueueHealthCheck(this IHealthChecksBuilder healthCheckBuilder)
    {
        return healthCheckBuilder.AddRabbitMQ(
            name: "ticketing-mq-check",
            tags: new string[] { "ready", "ticketing-mq" },
            setup: (sp, options) =>
            {
                MessageBrokerSettings settings = sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value;

                options.ConnectionUri = new Uri(settings.ConnectionString);
            }
        );
    }
}
