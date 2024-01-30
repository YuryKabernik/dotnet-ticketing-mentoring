using Microsoft.Extensions.Options;
using Ticketing.Notification.Common.Settings;

namespace Ticketing.Notification.Service.Settings;

internal class EmailProviderConfigureOptions : IConfigureOptions<MessageBrokerSettings>
{
    private readonly IConfiguration _configuration;

    public EmailProviderConfigureOptions(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public void Configure(MessageBrokerSettings options)
    {
        this._configuration.GetSection(EmailProviderConfiguration.SectionName).Bind(options);
    }
}
