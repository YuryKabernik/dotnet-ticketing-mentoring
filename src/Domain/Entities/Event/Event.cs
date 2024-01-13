namespace Ticketing.Domain.Entities.Event;

public class Event
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }

    public virtual required Venue.Venue Venue { get; set; }

    public required DateTime DateTime { get; set; }
}
