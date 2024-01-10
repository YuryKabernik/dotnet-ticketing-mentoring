using MediatR;
using Ticketing.Application.Feature.Payments.Responses;

namespace Ticketing.Application.Feature.Payments.Requests;

public record PaymentByIdRequest(Guid PaymentId) : IRequest<PaymentStatusResponse>;
