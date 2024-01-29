using Microsoft.Extensions.Options;
using Ticketing.Notification.Common;
using Ticketing.Notification.Service.HttpPolicies;
using Ticketing.Notification.Service.Providers.Email;
using Ticketing.Notification.Service.Providers.Email.Interfaces;
using Ticketing.Notification.Service.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<EmailProviderConfiguration>(
    builder.Configuration.GetSection(EmailProviderConfiguration.SectionName)
);

builder.Services.AddNotifications(builder.Configuration);
builder.Services
    .AddHttpClient<IEmailProvider, EmailProvider>(ConfigureEmailProvider)
    .AddPolicyHandler(RetryHttpPolicy.Policy);

var host = builder.Build();
host.Run();

static void ConfigureEmailProvider(IServiceProvider sp, HttpClient client)
{
    var configuration = sp.GetRequiredService<IOptions<EmailProviderConfiguration>>().Value;

    client.BaseAddress = new Uri(configuration.BaseAddress);
    client.DefaultRequestHeaders.Authorization = configuration.AuthenticationHeader;
}
