using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi.Payments;

/// <summary>
/// Describes Payment resources.
/// </summary>
[ApiController]
[Route("api/payments/{payment_id:alpha}")]
public class PaymentController : ControllerBase
{
    private PaymentStatus PaymentInfo;

    /// <summary>
    /// Initializes DI services.
    /// </summary>
    public PaymentController()
    {
        this.PaymentInfo = PaymentStatus.InProgress;
    }

    /// <summary>
    ///     Provides information of the current payment status.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns>
    ///     Returns the status of a payment   
    /// </returns>
    [ProducesResponseType<PaymentStatus>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public IResult GetStatus(string paymentId)
    {
        return TypedResults.Ok(this.PaymentInfo);
    }

    /// <summary>
    ///     Updates payment status and moves all the seats related to a payment to the sold state.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    [HttpPost("complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult UpdateToComplete(string paymentId)
    {
        this.PaymentInfo = PaymentStatus.Complete;

        return TypedResults.NoContent();
    }

    /// <summary>
    ///     Updates payment status and moves all the seats related to a payment to the available state.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    [HttpPost("failed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult UpdateToFailed(string paymentId)
    {
        this.PaymentInfo = PaymentStatus.Failed;

        return TypedResults.NoContent();
    }
}
