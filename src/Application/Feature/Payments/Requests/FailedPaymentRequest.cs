namespace Ticketing.Application.Feature.Payments.Requests;

public record FailPaymentRequest
{
    public Guid PaymentId { get; internal set; }
}
