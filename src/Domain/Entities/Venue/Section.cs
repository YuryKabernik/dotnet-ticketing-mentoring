namespace Ticketing.Domain.Entities.Venue;

public class Section
{
    public required int Id { get; set; }

    public required string Label { get; set; }

    public required virtual Venue Venue { get; set; } = null!;
}
