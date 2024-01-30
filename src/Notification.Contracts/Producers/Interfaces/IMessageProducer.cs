namespace Ticketing.Notification.Contracts.Producers.Interfaces;

public interface IMessageProducer
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellation) where TMessage : class;
}
