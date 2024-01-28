using Ticketing.Notification.Common.Messages.Content;

namespace Ticketing.Notification.Common.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync(NotificationMessage<EmailContent> emailNotificationMessage, CancellationToken cancellationToken);
}
