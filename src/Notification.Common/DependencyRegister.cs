using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ticketing.Notification.Common.Interfaces;
using Ticketing.Notification.Common.Producers;
using Ticketing.Notification.Common.Settings;

namespace Ticketing.Notification.Common;

public static class DependencyRegister
{
    public static IServiceCollection AddNotifications(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var assembly = Assembly.GetEntryAssembly();

        services.Configure<MessageBrokerSettings>(
            configuration.GetSection(MessageBrokerSettings.SectionName)
        );

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

        services.AddScoped<IMessagePublisher, MessageProducer>();

        return services;
    }
}
