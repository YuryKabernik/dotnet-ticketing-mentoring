using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi.Payments;

/// <summary>
/// Describes Payment resources.
/// </summary>
[Route("/payments/{payment_id}")]
[ApiController]
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
    [HttpGet]
    public Results<Ok<PaymentStatus>, NotFound> GetStatus(string paymentId)
    {
        return TypedResults.Ok(this.PaymentInfo);
    }

    /// <summary>
    ///     Updates payment status and moves all the seats related to a payment to the sold state.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <returns></returns>
    [HttpPost("complete")]
    public Results<NoContent, NotFound, BadRequest> UpdateToComplete(string paymentId)
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
    public Results<NoContent, NotFound, BadRequest> UpdateToFailed(string paymentId)
    {
        this.PaymentInfo = PaymentStatus.Failed;

        return TypedResults.NoContent();
    }
}
