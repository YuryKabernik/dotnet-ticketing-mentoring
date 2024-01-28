namespace Ticketing.Notification.Common.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellation) where TMessage : class;
}
