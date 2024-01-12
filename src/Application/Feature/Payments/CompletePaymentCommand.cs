﻿using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Payments;

public class CompletePaymentCommand : ICommandHandler<CompletePaymentRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentRepository _paymentRepository;

    public CompletePaymentCommand(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
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
    public Task Handle(CompletePaymentRequest request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(CompletePaymentRequest request, CancellationToken cancellation)
    {
        var payment = await this._paymentRepository.GetWithSeatsAsync(request.PaymentId, cancellation);

        if (payment is null)
            throw new NotFoundException($"Payment {request.PaymentId} is not found.");

        payment.Complete();

        await this._unitOfWork.SaveChanges(cancellation);
    }
}