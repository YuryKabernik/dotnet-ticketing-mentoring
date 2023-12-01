namespace Ticketing.Domain.Entities.Event;

public class EventSection
{
    public required int Id { get; set; }

    public required virtual Event Event { get; set; } = null!;
}
