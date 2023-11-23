namespace Ticketing.DataAccess.Models.Event;

public class Event
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int VenueId { get; set; }
    public required DateTime Date { get; set; }
}
