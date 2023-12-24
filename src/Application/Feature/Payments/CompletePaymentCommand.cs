
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Domain;
using Ticketing.Domain.Enums;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public class CompletePaymentCommand : ICommandHandler<CompletePaymentRequest>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IPaymentRepository paymentRepository;

    public CompletePaymentCommand(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        this.paymentRepository = paymentRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CompletePaymentRequest request, CancellationToken cancellation)
    {
        var payment = await this.paymentRepository.GetWithSeatsAsync(request.PaymentId, cancellation);

        payment.Complete();
        
        await this.unitOfWork.SaveChanges(cancellation);
    }
}
