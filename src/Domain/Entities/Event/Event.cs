namespace Ticketing.Domain.Entities.Event;

public class Event
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }

    public required virtual Venue.Venue Venue { get; set; } = null!;

    public required DateTime Date { get; set; }
}
