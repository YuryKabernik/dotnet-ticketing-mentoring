using MediatR;
using Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;
using Ticketing.Application.ObjectMapping;
using Ticketing.Notification.Contracts;
using Ticketing.Notification.Contracts.Messages.Content;
using Ticketing.Notification.Contracts.Producers.Interfaces;

namespace Ticketing.Application.Notifications.Email;

internal class EmailNotificationHandler : INotificationHandler<SeatsBookedNotification>
{
    private readonly IMessageProducer publisher;

    public EmailNotificationHandler(IMessageProducer publisher)
    {
        this.publisher = publisher;
    }

    public async Task Handle(SeatsBookedNotification notification, CancellationToken cancellationToken)
    {
        NotificationMessage<EmailExpectingPayment> emailNotificationMessage = notification.ToEmailMessage();

        emailNotificationMessage.Operation = nameof(EmailExpectingPayment);
        emailNotificationMessage.Parameters.Add("customer id", notification.Order.User.Id.ToString());
        emailNotificationMessage.Parameters.Add("customer name", notification.Order.User.Email);

        await this.publisher.PublishAsync(emailNotificationMessage, cancellationToken);
    }
}
