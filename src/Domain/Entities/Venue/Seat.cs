namespace Ticketing.Domain.Entities.Venue;

public class Seat
{
    public required int Id { get; set; }

    public required string Label { get; set; }

    public required virtual Row Row { get; set; } = null!;
}
