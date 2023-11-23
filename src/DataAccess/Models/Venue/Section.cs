namespace Ticketing.DataAccess.Models.Venue;

public class Section
{
    public required int Id { get; set; }
    public required string Label { get; set; }
    public required int VenueId { get; set; }
}
