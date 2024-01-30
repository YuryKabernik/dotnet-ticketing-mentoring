using MassTransit;
using Microsoft.Extensions.Logging;
using Ticketing.Notification.Contracts.Producers.Interfaces;

namespace Ticketing.Notification.Contracts.Producers;

internal class MessageProducer : IMessageProducer
{
    private const string Message = "Message of type {type} was not sent due to an unhandled exception: {exception}";

    private readonly IBus _bus;
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(IBus bus, ILogger<MessageProducer> logger)
    {
        this._bus = bus;
        this._logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellation) where TMessage : class
    {
        try
        {
            await this._bus.Publish(message, cancellation);
        }
        catch (Exception exception)
        {
            var type = typeof(TMessage).FullName ?? typeof(TMessage).Name;

            this._logger.LogCritical(Message, type, exception);
        }
    }
}
