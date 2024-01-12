using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;
using Ticketing.Domain.Exceptions;

namespace Ticketing.Domain.Entities.Ordering;

public class Order
{
    public int Id { get; set; }
    public OrderStatusOption Status { get; set; }

    public virtual User? User { get; set; }
    public virtual Payment? Payment { get; set; }

    public virtual ICollection<EventSeat> Seats { get; set; } = new List<EventSeat>();

    public static Order CreateFrom(Cart cart)
    {
        if (cart.Seats.Count < 1)
            throw new NotFoundException($"Invalid number of seats in the Cart {cart.Guid} to create an Order.");

        return new Order
        {
            Seats = cart.Seats,
            Payment = new()
            {
                Price = cart.FinalPrice
            }
        };
    }
}
