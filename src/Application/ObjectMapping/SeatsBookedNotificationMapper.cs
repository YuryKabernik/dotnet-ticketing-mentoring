using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Notification.Contracts;
using Ticketing.Notification.Contracts.Messages.Content;

namespace Ticketing.Application.ObjectMapping;

[Mapper]
public static partial class SeatsBookedNotificationMapper
{
    [MapProperty(nameof(@SeatsBookedNotification.Order.User), nameof(@NotificationMessage<EmailExpectingPayment>.Content.Recipient))]
    [MapProperty(nameof(@SeatsBookedNotification.Order.Payment), nameof(@NotificationMessage<EmailExpectingPayment>.Content.Order))]
    public static partial NotificationMessage<EmailExpectingPayment> ToEmailMessage(this SeatsBookedNotification notification);

    [MapProperty(nameof(User.Name), nameof(RecipientInfo.Name))]
    [MapProperty(nameof(User.Email), nameof(RecipientInfo.Email))]
    private static partial RecipientInfo ToRecipientInfo(this User user);

    [MapProperty(nameof(@Payment.Order.Id), nameof(@RecipientsOrder.OrderId))]
    [MapProperty(nameof(@Payment.PaymentGuid), nameof(@RecipientsOrder.PaymentId))]
    [MapProperty(nameof(@Payment.Price), nameof(@RecipientsOrder.Amount))]
    private static partial RecipientsOrder ToRecipientsOrder(this Payment payment);
}
