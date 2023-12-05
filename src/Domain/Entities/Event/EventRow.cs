namespace Ticketing.Domain.Entities.Event;

public class EventRow
{
    public required int Id { get; set; }

    public virtual EventSection? Section { get; set; }
}
