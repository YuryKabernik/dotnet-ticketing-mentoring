using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public virtual User? User { get; set; }

    public virtual EventSeat? Seat { get; set; }
}
