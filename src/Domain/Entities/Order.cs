using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public virtual User? User { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ICollection<EventSeat>? Seats { get; set; } = new List<EventSeat>();
}
