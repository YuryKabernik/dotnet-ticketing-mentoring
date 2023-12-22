using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Event;

public class EventSeat
{
    public required int Id { get; set; }

    public required SeatStatusOption Status { get; set; }

    public virtual EventRow? Row { get; set; }

    public virtual Price? Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Payment? Payment { get; set; }
}
