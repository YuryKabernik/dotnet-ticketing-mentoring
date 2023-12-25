using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public class FailPaymentCommand : ICommandHandler<FailPaymentRequest>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FailPaymentCommand(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(FailPaymentRequest request, CancellationToken cancellation)
    {
        var payment = await this._paymentRepository.GetWithSeatsAsync(request.PaymentId, cancellation);

        payment.Fail();

        await this._unitOfWork.SaveChanges(cancellation);
    }
}
