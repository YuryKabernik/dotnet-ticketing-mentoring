namespace Ticketing.Notification.Contracts.Settings;

public class MessageBrokerSettings
{
    public const string SectionName = "MassageBroker";

    public string ConnectionString { get; set; } = string.Empty;
}
