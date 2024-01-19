using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application;
using Ticketing.Application.Feature.Carting.BookCartSeats;
using Ticketing.Application.Feature.Carting.QueryCart;
using Ticketing.Application.Feature.Carting.RemoveCartSeat;
using Ticketing.Application.Feature.Carting.UpdateCartSeats;
using Ticketing.WebApi.Models;
using Ticketing.WebApi.Orders.Mappers;
using Ticketing.WebApi.Orders.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Order resources.
/// </summary>
[ApiController]
[Route("api/orders/carts/{cartId:guid}")]
public class OrderController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Returns a list of items in a cart.
    /// </summary>
    /// <param name="cartId">
    ///     A cart id is a guid, generated and stored on the client side.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<CartInfo>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetCart(Guid cartId, CancellationToken cancellationToken)
    {
        CartQuery query = new(cartId);
        CartQueryResponse response = await mediator.Send(query, cancellationToken);
        CartInfo cartInfo = response.ToCartInfo();

        return TypedResults.Ok(cartInfo);
    }

    /// <summary>
    ///     Adds a seat to the cart.
    /// </summary>
    /// <param name="cartId">A cart guid.</param>
    /// <param name="seatDetails">An object of event_id, seat_id and price_id as a payload.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns a cart state (with total amount) back to the caller.    
    /// </returns>
    [HttpPost]
    [ProducesResponseType<CartInfo>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> PostCart(
        [FromRoute] Guid cartId,
        [FromBody] SeatSelection seatDetails,
        CancellationToken cancellationToken)
    {
        UpdateCartCommand command = new(cartId, seatDetails.ToSeatPayload());
        await mediator.Send(command, cancellationToken);

        return await this.GetCart(cartId, cancellationToken);
    }

    /// <summary>
    ///     Deletes a seat for a specific cart.
    /// </summary>
    /// <param name="cartId">A cart guid.</param>
    /// <param name="eventId"></param>
    /// <param name="seatId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("events/{eventId:int}/seats/{seatId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteSeat(Guid cartId, int eventId, int seatId, CancellationToken cancellationToken)
    {
        RemoveCartSeatCommand command = new(cartId, eventId, seatId);
        await mediator.Send(command, cancellationToken);

        return TypedResults.Ok();
    }

    /// <summary>
    ///     Moves all the seats in the cart to a booked state.
    /// </summary>
    /// <param name="cartId">A cart guid.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns a payment id.
    /// </returns>
    [HttpPut("book")]
    [ProducesResponseType<PaymentInfo>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> BookAll(Guid cartId, CancellationToken cancellationToken)
    {
        BookCartSeatsCommand command = new(cartId);
        BookCartSeatsResponse response = await mediator.Send(command, cancellationToken);

        return TypedResults.Ok<PaymentInfo>(new(response.PaymentId));
    }
}
