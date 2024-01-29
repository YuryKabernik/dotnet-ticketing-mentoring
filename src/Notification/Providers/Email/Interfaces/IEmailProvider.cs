using Ticketing.Notification.Service.Email;

namespace Ticketing.Notification.Service.Providers.Email.Interfaces;

public interface IEmailProvider
{
    Task SendAsync(SendEmailCommand emailMessage, CancellationToken cancellationToken);
}
