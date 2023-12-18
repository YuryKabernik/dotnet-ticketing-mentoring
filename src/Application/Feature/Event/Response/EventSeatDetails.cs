namespace Ticketing.Application;

public class EventSeatDetails
{
    public int SectionId { get; set; }
    public int RowId { get; set; }
    public int SeatId { get; set; }
    public SeatStatus? Status { get; set; }
    public PriceOption? Price { get; set; }
}

public class PriceOption
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}

public class SeatStatus
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
