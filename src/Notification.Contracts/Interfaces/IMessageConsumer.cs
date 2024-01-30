using MassTransit;

namespace Ticketing.Notification.Contracts.Interfaces;

public interface IMessageConsumer<TContent> :
    IConsumer<NotificationMessage<TContent>> where TContent : class
{
}
