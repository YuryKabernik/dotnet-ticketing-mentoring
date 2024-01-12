using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Event;

public class EventSeat
{
    public required int Id { get; init; }

    public required SeatStatusOption Status { get; set; }

    public virtual required EventRow Row { get; init; }

    public virtual required Price Price { get; init; }

    public virtual Order? Order { get; init; }

    public virtual Cart? Cart { get; init; }

    public virtual Payment? Payment { get; init; }

    public bool IsExact(int seatId, int eventId, int priceId)
    {
        return this.Id == seatId &&
               this.Price.Id == priceId &&
               this.Row.Section.Event.Id == eventId;
    }
}