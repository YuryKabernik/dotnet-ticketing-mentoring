using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Payments;

public class Payment
{
    public int Id { get; set; }
    public Guid PaymentGuid { get; set; }
    public required PaymentStatusOption Status { get; set; }

    public virtual IEnumerable<EventSeat> Seats { get; set; } = new List<EventSeat>();
}
