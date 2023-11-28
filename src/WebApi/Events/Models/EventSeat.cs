namespace Ticketing.WebApi;

public record EventSeat(int SectionId, int RowId, int SeatId, SeatStatus Status, SeatPriceOption PriceOption);
public record SeatStatus(int Id, string Name);
public record SeatPriceOption(int Id, string Name);
