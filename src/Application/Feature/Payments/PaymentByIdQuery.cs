using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public record PaymentStatusResponse(string Status);

public class PaymentByIdQuery : IQueryHandler<PaymentByIdRequest, PaymentStatusResponse>
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentByIdQuery(IPaymentRepository paymentRepository)
    {
        this._paymentRepository = paymentRepository;
    }

    public async Task<PaymentStatusResponse> ExecuteAsync(PaymentByIdRequest request, CancellationToken cancellation)
    {
        var payment = await _paymentRepository.GetAsync(request.PaymentId, cancellation);
        var statusName = Enum.GetName(payment!.Status);

        return new(statusName!);
    }
}
