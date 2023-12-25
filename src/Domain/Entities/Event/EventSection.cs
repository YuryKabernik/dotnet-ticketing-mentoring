namespace Ticketing.Domain.Entities.Event;

public class EventSection
{
    public required int Id { get; set; }

    public virtual required Event Event { get; set; }
}
