using Ticketing.Application.CQRS;
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
        this._paymentRepository = paymentRepository;
        this._unitOfWork = unitOfWork;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Handle(FailPaymentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(FailPaymentRequest request, CancellationToken cancellation)
    {
        var payment = await this._paymentRepository.GetWithSeatsAsync(request.PaymentId, cancellation);

        payment.Fail();

        await this._unitOfWork.SaveChanges(cancellation);
    }
}
