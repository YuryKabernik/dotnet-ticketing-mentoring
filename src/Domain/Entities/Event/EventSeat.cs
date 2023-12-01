namespace Ticketing.Domain.Entities.Event;

public class EventSeat
{
    public required int Id { get; set; }

    public required virtual EventRow Row { get; set; } = null!;

    public required decimal Price { get; set; }
}
