using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual ICollection<EventSeat> Seats { get; set; } = new List<EventSeat>();
}
