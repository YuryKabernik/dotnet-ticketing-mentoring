using MediatR;

namespace Ticketing.Application.Feature.Payments.Requests;

public record CompletePaymentRequest(Guid PaymentId) : IRequest;
