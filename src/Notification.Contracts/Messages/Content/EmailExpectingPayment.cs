namespace Ticketing.Notification.Contracts.Messages.Content;

public class EmailExpectingPayment
{
    public required RecipientInfo Recipient { get; set; }

    public required RecipientsOrder Order { get; set; }
}

public class RecipientInfo
{
    public required string Name { get; set; }

    public required string Email { get; set; }
}

public class RecipientsOrder
{
    public required decimal Amount { get; set; }

    public required Guid PaymentId { get; set; }

    public required int OrderId { get; set; }
}
