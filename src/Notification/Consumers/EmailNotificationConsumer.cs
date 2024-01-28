using System.Text.Json;
using MassTransit;
using Ticketing.Notification.Common;
using Ticketing.Notification.Common.Messages.Content;

namespace Ticketing.Notification.Service.Consumers;

public class EmailNotificationConsumer : IConsumer<NotificationMessage<EmailContent>>
{
    private readonly ILogger<EmailNotificationConsumer> _logger;

    public EmailNotificationConsumer(ILogger<EmailNotificationConsumer> logger)
    {
        this._logger = logger;
    }

    public Task Consume(ConsumeContext<NotificationMessage<EmailContent>> context)
    {
        var message = JsonSerializer.Serialize(context.Message.Content);

        this._logger.LogInformation("Email Received: {message}", message);

        return Task.CompletedTask;
    }
}
