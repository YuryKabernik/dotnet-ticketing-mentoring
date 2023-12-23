namespace Ticketing.Application.Feature.Payments.Requests;

public record CompletePaymentRequest
{
    public Guid PaymentId { get; internal set; }
}
