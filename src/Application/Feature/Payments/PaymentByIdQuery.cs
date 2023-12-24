using Ticketing.Application.CQRS;
using Ticketing.Domain;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public record PaymentByIdRequest(Guid PaymentId);
public record PaymentStatusResponse(string Status);

public class PaymentByIdQuery : IQueryHandler<PaymentByIdRequest, PaymentStatusResponse>
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentByIdQuery(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<PaymentStatusResponse> ExecuteAsync(PaymentByIdRequest request, CancellationToken cancellation)
    {
        var payment = await paymentRepository.GetAsync(request.PaymentId, cancellation);
        NotFoundException.ThrowIfNull(payment!, $"Payment {request.PaymentId} is not found.");

        var statusName = Enum.GetName(payment!.Status);

        return new(statusName!);
    }
}
