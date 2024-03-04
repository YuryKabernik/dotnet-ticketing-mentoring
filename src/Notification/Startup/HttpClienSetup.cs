using Microsoft.Extensions.Options;
using Ticketing.Notification.Service.Providers.Email;
using Ticketing.Notification.Service.Providers.Email.Interfaces;
using Ticketing.Notification.Service.Settings;

namespace Ticketing.Notification.Startup;

public static class HttpClienSetup
{
    public static IHttpClientBuilder AddEmailProviderHttpClient(this IServiceCollection services) =>
        services.AddHttpClient<IEmailProvider, EmailProvider>((serviceProvider, client) =>
        {
            var configuration = serviceProvider.GetRequiredService<IOptions<EmailProviderConfiguration>>().Value;

            client.BaseAddress = new Uri(configuration.BaseAddress);
            client.DefaultRequestHeaders.Authorization = configuration.AuthenticationHeader;
        });

}