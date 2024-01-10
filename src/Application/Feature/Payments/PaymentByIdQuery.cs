using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Application.Feature.Payments.Responses;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;


public class PaymentByIdQuery : IQueryHandler<PaymentByIdRequest, PaymentStatusResponse>
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentByIdQuery(IPaymentRepository paymentRepository)
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
    public Task<PaymentStatusResponse> Handle(PaymentByIdRequest request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<PaymentStatusResponse> ExecuteAsync(PaymentByIdRequest request, CancellationToken cancellation)
    {
        var payment = await this._paymentRepository.GetAsync(request.PaymentId, cancellation);
        var statusName = Enum.GetName(payment!.Status);

        return new(statusName!);
    }

}
