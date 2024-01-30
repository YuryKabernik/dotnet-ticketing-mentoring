using MassTransit;
using Microsoft.Extensions.Options;
using Ticketing.Notification.Contracts;
using Ticketing.Notification.Contracts.Interfaces;
using Ticketing.Notification.Contracts.Messages.Content;
using Ticketing.Notification.Service.Email;
using Ticketing.Notification.Service.Providers.Email;
using Ticketing.Notification.Service.Providers.Email.Interfaces;
using Ticketing.Notification.Service.Settings;

namespace Ticketing.Notification.Service.Consumers;

public class EmailNotificationConsumer : IMessageConsumer<EmailExpectingPayment>
{
    private const string EmailSubject = "Ticketing: Tickets Booked";
    private const string EmailBodyTemplate = "Thank you {0} for using our Ticketing application! The amount to pay is {1} by the payment number {2}.";

    private readonly IEmailProvider _emailProvider;
    private readonly SenderSettings _senderOptions;

    public EmailNotificationConsumer(
        IEmailProvider emailProvider,
        IOptions<EmailProviderConfiguration> options)
    {
        this._emailProvider = emailProvider;
        this._senderOptions = options.Value.Sender;
    }

    public async Task Consume(ConsumeContext<NotificationMessage<EmailExpectingPayment>> context)
    {
        var command = new SendEmailCommand(this.ToEmailMessage(context.Message.Content));
        await this._emailProvider.SendAsync(command, CancellationToken.None);
    }

    private EmailMessage ToEmailMessage(EmailExpectingPayment context)
    {
        string emailBody = string.Format(
            EmailBodyTemplate,
            context.Recipient.Name,
            context.Order.Amount,
            context.Order.PaymentId
        );

        EmailMessage email = new()
        {
            From = new()
            {
                Name = this._senderOptions.Name,
                Email = this._senderOptions.Email
            },
            To = new List<EmailToSection>()
            {
                new() {
                    Email = context.Recipient.Email,
                    Name = context.Recipient.Name,
                }
            },
            Subject = EmailSubject,
            TextPart = emailBody,
            HTMLPart = emailBody
        };

        return email;
    }
}
