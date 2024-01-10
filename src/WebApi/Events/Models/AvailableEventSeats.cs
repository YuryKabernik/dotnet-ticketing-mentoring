namespace Ticketing.WebApi.Events.Models;

/// <summary>
/// A seats available for booking.
/// </summary>
/// <param name="Seats"></param>
public record AvailableEventSeats(IEnumerable<EventSeat> Seats);

/// <summary>
/// A seat available for booking.
/// </summary>
/// <param name="SectionId">
/// Section ID of the dedicated event section.    
/// </param>
/// <param name="RowId">
/// Row ID of the dedicated event row.    
/// </param>
/// <param name="SeatId">
/// Seat ID of the dedicated event seat.    
/// </param>
/// <param name="Status">
/// A status of the seat.
/// </param>
/// <param name="PriceOption">
/// A seat price.
/// </param>
public record EventSeat(int SectionId, int RowId, int SeatId, SeatStatus Status, SeatPriceOption PriceOption);

/// <summary>
/// A status of the seat.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record SeatStatus(int Id, string Name);

/// <summary>
/// A seat price.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record SeatPriceOption(int Id, string Name);
