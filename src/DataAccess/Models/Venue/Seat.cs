namespace Ticketing.DataAccess.Models.Venue;

public class Seat
{
    public required int Id { get; set; }
    public required string Label { get; set; }
    public required int RowId { get; set; }
}
