namespace Ticketing.Domain.Entities.Event;

public class EventSeat
{
    public required int Id { get; set; }

    public virtual EventRow? Row { get; set; }

    public virtual Price? Price { get; set; }
}
