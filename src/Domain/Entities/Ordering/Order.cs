using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Ordering;

public class Order
{
    public int Id { get; set; }
    public OrderStatusOption Status { get; set; }

    public virtual User? User { get; set; }
    public virtual Payment? Payment { get; set; }

    public virtual ICollection<EventSeat>? Seats { get; set; } = new List<EventSeat>();

    public static Order CreateFrom(Cart cart) => new()
    {
        Seats = cart.Seats,
        Payment = new()
        {
            Price = cart.FinalPrice
        }
    };
}
