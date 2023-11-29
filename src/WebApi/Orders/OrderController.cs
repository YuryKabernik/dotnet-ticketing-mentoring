using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Order resources.
/// </summary>
[Route("orders/carts/{cartId:guid}")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly Dictionary<Guid, CartInfo> Carts;

    /// <summary>
    /// Initializes DI services.
    /// </summary>
    public OrderController()
    {
        this.Carts = new Dictionary<Guid, CartInfo>();
    }

    /// <summary>
    /// Returns a list of items in a cart.
    /// </summary>
    /// <param name="cartId">
    ///     A cart id is a uuid, generated and stored on the client side.
    /// </param>
    /// <returns></returns>
    [HttpGet]
    public Results<NotFound, Ok<CartInfo>> GetCart(Guid cartId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.Ok(this.Carts[cartId]);
        }

        return TypedResults.NotFound();
    }

    /// <summary>
    ///     Adds a seat to the cart.
    /// </summary>
    /// <param name="cartId">A cart uuid.</param>
    /// <param name="seatDetails">An object of event_id, seat_id and price_id as a payload.</param>
    /// <returns>
    ///     Returns a cart state (with total amount) back to the caller.    
    /// </returns>
    [HttpPost]
    public Results<BadRequest, Ok<CartInfo>> PostCart(Guid cartId, [FromBody] SeatSelection seatDetails)
    {
        return TypedResults.Ok(this.Carts[cartId]);
    }

    /// <summary>
    ///     Deletes a seat for a specific cart.
    /// </summary>
    /// <param name="cartId">A cart uuid.</param>
    /// <param name="eventId"></param>
    /// <param name="seatId"></param>
    /// <returns></returns>
    [HttpDelete("events/{eventId:int}/seats/{seatId:int}")]
    public Results<NotFound, NoContent> DeleteSeat(Guid cartId, int eventId, int seatId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }

    /// <summary>
    ///     Moves all the seats in the cart to a booked state.
    /// </summary>
    /// <param name="cartId">A cart uuid.</param>
    /// <returns>
    ///     Returns a payment id.
    /// </returns>
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
