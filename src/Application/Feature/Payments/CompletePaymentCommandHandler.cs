using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public class CompletePaymentCommandHandler : ICommandHandler<CompletePaymentRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentRepository _paymentRepository;

    public CompletePaymentCommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
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
    public async Task Handle(CompletePaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = await this._paymentRepository.GetWithSeatsAsync(request.PaymentId, cancellationToken);

        if (payment is null)
            throw new NotFoundException($"PaymentId {request.PaymentId} is not found.");

        payment.Complete();

        await this._unitOfWork.SaveChanges(cancellationToken);
    }
}
