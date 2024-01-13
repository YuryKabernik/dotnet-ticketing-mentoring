namespace Ticketing.Application.Feature.Event.Response;

public class EventSeatDetails
{
    public int SectionId { get; set; }
    public int RowId { get; set; }
    public int SeatId { get; set; }
    public required SeatStatus Status { get; set; }
    public required PriceOption Price { get; set; }
}

public record PriceOption(int Id, string Name);

public record SeatStatus(int Id, string Name);
