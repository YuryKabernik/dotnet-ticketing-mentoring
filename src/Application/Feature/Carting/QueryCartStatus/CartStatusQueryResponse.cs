using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Carting.QueryCartStatus;

/// <summary>
/// A cart state (with total amount) back to the caller.
/// </summary>
public record CartStatusQueryResponse(Guid CartId, IEnumerable<EventSeat> Seats)
{
    public int TotalSeats => Seats.Count();
    public decimal TotalAmount => Seats.Sum(seat => seat.Price!.Amount);
}
