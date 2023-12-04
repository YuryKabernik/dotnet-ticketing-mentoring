namespace Ticketing.Domain.Entities.Venue;

public class Section
{
    public required int Id { get; set; }

    public required string Label { get; set; }

    public virtual Venue? Venue { get; set; }
}
