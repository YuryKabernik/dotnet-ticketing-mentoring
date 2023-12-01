namespace Ticketing.Domain.Entities.Venue;

public class Row
{
    public required int Id { get; set; }

    public required string Label { get; set; }

    public required virtual Section Section { get; set; } = null!;
}
