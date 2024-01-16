using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.Feature.Payments.Requests;
using Ticketing.Application.Feature.Payments.Responses;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi.Payments;

/// <summary>
/// Describes Payment resources.
/// </summary>
[ApiController]
[Route("api/payments/{payment_id:guid}")]
public class PaymentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Provides information of the current payment status.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns the status of a payment   
    /// </returns>
    [ProducesResponseType<PaymentStatus>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IResult> GetStatus(Guid paymentId, CancellationToken cancellationToken)
    {
        PaymentByIdRequest request = new(paymentId);
        PaymentStatusResponse status = await mediator.Send(request, cancellationToken);

        return TypedResults.Ok<PaymentStatus>(new(status.Status));
    }

    /// <summary>
    ///     Updates payment status and moves all the seats related to a payment to the sold state.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateToComplete(Guid paymentId, CancellationToken cancellationToken)
    {
        CompletePaymentRequest request = new(paymentId);
        await mediator.Send(request, cancellationToken);

        return TypedResults.Ok();
    }

    /// <summary>
    ///     Updates payment status and moves all the seats related to a payment to the available state.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("failed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateToFailed(Guid paymentId, CancellationToken cancellationToken)
    {
        FailPaymentRequest request = new(paymentId);
        await mediator.Send(request, cancellationToken);

        return TypedResults.Ok();
    }
}
