using MediatR;

namespace Ticketing.Application.Feature.Payments.Requests;

public record FailPaymentRequest(Guid PaymentId) : IRequest;
