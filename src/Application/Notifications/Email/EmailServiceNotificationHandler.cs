using MediatR;
using Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Notification.Common;
using Ticketing.Notification.Common.Interfaces;
using Ticketing.Notification.Common.Messages.Content;

namespace Ticketing.Application.Notifications.Email;

internal class EmailServiceNotificationHandler : INotificationHandler<SeatsBookedNotification>
{
    private readonly IMessagePublisher publisher;

    public EmailServiceNotificationHandler(IMessagePublisher publisher)
    {
        this.publisher = publisher;
    }

    public async Task Handle(SeatsBookedNotification notification, CancellationToken cancellationToken)
    {
        NotificationMessage<EmailDetails> emailNotificationMessage = ToEmailMessage(notification);

        emailNotificationMessage.Parameters.Add("customer id", notification.Order.User.Id.ToString());
        emailNotificationMessage.Parameters.Add("customer name", notification.Order.User.Email);

        await this.publisher.PublishAsync(emailNotificationMessage, cancellationToken);
    }

    private static NotificationMessage<EmailDetails> ToEmailMessage(SeatsBookedNotification notification) =>
       new()
       {
           Operation = notification.GetType().Name,
           Content = new()
           {
               Recipient = GetRecipientInfo(notification.Order.User),
               Order = GetRecipientOrder(notification.Order.Payment)
           }
       };

    private static RecipientInfo GetRecipientInfo(User user) =>
        new() { Name = user.Name, Email = user.Email };

    private static RecipientsOrder GetRecipientOrder(Payment payment) =>
        new() { Amount = payment.Price, PaymentId = payment.PaymentGuid };
}
