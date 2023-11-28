using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

[Route("orders/carts/{cartId:guid}")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly Dictionary<Guid, CartInfo> Carts;

    public OrderController()
    {
        this.Carts = new Dictionary<Guid, CartInfo>();
    }

    [HttpGet]
    public Results<NotFound, Ok<CartInfo>> GetCart(Guid cartId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.Ok(this.Carts[cartId]);
        }

        return TypedResults.NotFound();
    }

    [HttpPost]
    public Results<BadRequest, Ok<CartInfo>> PostCart(Guid cartId, [FromBody] SelectedSeatBody selectedSeat)
    {
        return TypedResults.Ok(this.Carts[cartId]);
    }

    [HttpDelete("events/{eventId:int}/seats/{seatId:int}")]
    public Results<NotFound, NoContent> DeleteSeat(Guid cartId, int eventId, int seatId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }

    [HttpPut("book")]
    public Results<NotFound, Ok<PaymentInfo>> BookAll(Guid cartId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.Ok<PaymentInfo>(default);
        }

        return TypedResults.NotFound();
    }
}
