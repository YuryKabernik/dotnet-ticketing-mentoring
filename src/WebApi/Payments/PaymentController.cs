using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi.Payments;

[Route("/payments/{payment_id}")]
[ApiController]
public class PaymentController : ControllerBase
{
    private PaymentStatus PaymentInfo;

    public PaymentController()
    {
        this.PaymentInfo = PaymentStatus.InProgress;
    }

    [HttpGet]
    public Results<Ok<PaymentStatus>, NotFound> GetStatus(string paymentId)
    {
        return TypedResults.Ok(this.PaymentInfo);
    }

    [HttpPost("complete")]
    public Results<NoContent, NotFound, BadRequest> UpdateToComplete(string paymentId)
    {
        this.PaymentInfo = PaymentStatus.Complete;

        return TypedResults.NoContent();
    }

    [HttpPost("failed")]
    public Results<NoContent, NotFound, BadRequest> UpdateToFailed(string paymentId)
    {
        this.PaymentInfo = PaymentStatus.Failed;

        return TypedResults.NoContent();
    }
}
