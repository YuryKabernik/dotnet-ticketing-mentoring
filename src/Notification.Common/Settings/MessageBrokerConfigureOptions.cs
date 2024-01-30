using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ticketing.Notification.Common.Settings;

/// <summary>
/// <inheritdoc/>
/// 
/// More info here: https://andrewlock.net/simplifying-dependency-injection-for-iconfigureoptions-with-the-configureoptions-helper/
/// 
/// </summary>
public class MessageBrokerConfigureOptions : IConfigureOptions<MessageBrokerSettings>
{
    private readonly IConfiguration _configuration;

    public MessageBrokerConfigureOptions(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public void Configure(MessageBrokerSettings options)
    {
        this._configuration.GetSection(MessageBrokerSettings.SectionName).Bind(options);
    }
}
