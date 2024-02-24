using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ticketing.DataAccess.Repositories;
using Ticketing.DataAccess.Setup;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.DependencyInjection;

public static class DependencyRegistry
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseConfigureOptions>();

        services.AddDbContext<DataContext>();

        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventSeatRepository, EventSeatRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();

        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<DataContext>()
        );

        return services;
    }

    public static IHealthChecksBuilder AddDataAccessHealthCheck(this IHealthChecksBuilder healthCheckBuilder)
    {
        return healthCheckBuilder.AddSqlServer(
            name: "ticketing-db-check",
            tags: new string[] { "ready", "ticketing-db" },
            connectionStringFactory: sp =>
            {
                DatabaseSettings settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;

                return settings.ConnectionString;
            }
        );
    }
}
