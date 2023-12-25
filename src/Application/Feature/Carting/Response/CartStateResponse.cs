using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Cart.Response;

/// <summary>
/// A cart state (with total amount) back to the caller.
/// </summary>
public record CartStateResponse(Guid CartId, IEnumerable<EventSeat> Seats)
{
    public int TotalSeats => Seats.Count();
    public decimal TotalAmount => Seats.Sum(seat => seat.Price!.Amount);
}
