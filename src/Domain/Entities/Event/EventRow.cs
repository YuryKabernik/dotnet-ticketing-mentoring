﻿namespace Ticketing.Domain.Entities.Event;

public class EventRow
{
    public required int Id { get; set; }

    public virtual required EventSection Section { get; set; }
}
