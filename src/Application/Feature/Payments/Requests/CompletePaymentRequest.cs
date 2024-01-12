using MediatR;

namespace Ticketing.Application.Feature.Payments.Requests;

public record CompletePaymentRequest : IRequest
{
    public Guid PaymentId { get; internal set; }
}
