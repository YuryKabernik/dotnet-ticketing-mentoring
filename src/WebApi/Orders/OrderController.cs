using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Order resources.
/// </summary>
[ApiController]
[Route("api/orders/carts/{cartId:guid}")]
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
    [ProducesResponseType<CartInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult GetCart(Guid cartId)
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
    [ProducesResponseType<CartInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult PostCart(Guid cartId, [FromBody] SeatSelection seatDetails)
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult DeleteSeat(Guid cartId, int eventId, int seatId)
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
    [ProducesResponseType<PaymentInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult BookAll(Guid cartId)
    {
        if (this.Carts.ContainsKey(cartId))
        {
            return TypedResults.Ok<PaymentInfo>(default);
        }

        return TypedResults.NotFound();
    }
}
