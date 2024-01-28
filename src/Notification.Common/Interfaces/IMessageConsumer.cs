using MassTransit;

namespace Ticketing.Notification.Common.Interfaces;

public interface IMessageConsumer<TContent> :
    IConsumer<NotificationMessage<TContent>> where TContent : class
{
}
