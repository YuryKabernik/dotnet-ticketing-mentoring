using MassTransit;
using Ticketing.Notification.Common;
using Ticketing.Notification.Common.Messages.Content;

namespace Ticketing.Notification.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        this._logger = logger;
        this._bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (this._logger.IsEnabled(LogLevel.Information))
            {
                var message = string.Format("Worker running at: {0}", DateTimeOffset.Now);

                this._logger.LogInformation("Sent: {message}", message);

                await this._bus.Publish(new NotificationMessage<EmailContent> { Content = new() }, cancellationToken: stoppingToken);
            }
            await Task.Delay(2000, stoppingToken);
        }
    }
}
