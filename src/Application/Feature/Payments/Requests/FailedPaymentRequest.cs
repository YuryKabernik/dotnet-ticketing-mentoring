using MediatR;

namespace Ticketing.Application.Feature.Payments.Requests;

public record FailPaymentRequest : IRequest
{
    public Guid PaymentId { get; internal set; }
}
