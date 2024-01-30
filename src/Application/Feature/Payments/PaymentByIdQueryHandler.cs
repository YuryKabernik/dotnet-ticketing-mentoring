using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Application.Feature.Payments.Responses;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public class PaymentByIdQueryHandler : IQueryHandler<PaymentByIdRequest, PaymentStatusResponse>
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentByIdQueryHandler(IPaymentRepository paymentRepository)
    {
        this._paymentRepository = paymentRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<PaymentStatusResponse> Handle(PaymentByIdRequest request, CancellationToken cancellationToken)
    {
        var payment = await this._paymentRepository.GetAsync(request.PaymentId, cancellationToken);

        if (payment is null)
            throw new NotFoundException($"PaymentId {request.PaymentId} was not found.");

        var statusName = Enum.GetName(payment.Status);

        if (statusName is null)
            throw new InvalidCastException($"PaymentId status {payment.Status} match was not found.");

        return new PaymentStatusResponse(statusName);
    }
}
