﻿using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Ordering;

public class Order
{
    public int Id { get; set; }
    public OrderStatusOption Status { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<EventSeat>? Seats { get; set; } = new List<EventSeat>();
}
