using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Event;

public class EventSeat
{
    public required int Id { get; set; }

    public required SeatStatusOption Status { get; set; }

    public virtual required EventRow Row { get; set; }

    public virtual required Price Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Payment? Payment { get; set; }

    public bool IsExact(int seatId, int eventId, int priceId)
    {
        return this.Id == seatId &&
               this.Price.Id == priceId &&
               this.Row.Section.Event.Id == eventId;
    }
}