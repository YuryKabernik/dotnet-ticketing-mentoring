using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<EventSeat> Seats { get; set; } = new List<EventSeat>();

    public decimal FinalPrice
    {
        get => this.Seats?.Sum(seat => seat.Price?.Amount) ?? 0;
    }

    public void BookSeats()
    {
        foreach (var seat in this.Seats)
        {
            seat.Status = SeatStatusOption.Booked;
        }
    }

    public void Clear()
    {
        this.Seats.Clear();
    }
}
