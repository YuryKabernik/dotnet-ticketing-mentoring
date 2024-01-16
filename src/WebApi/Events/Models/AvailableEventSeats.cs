namespace Ticketing.WebApi.Events.Models;

/// <summary>
/// A seats available for booking.
/// </summary>
/// <param name="Seats"></param>
public record AvailableEventSeats(IEnumerable<AvailableEventSeat> Seats);

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
/// <param name="Price">
/// A seat price.
/// </param>
public record AvailableEventSeat(int SectionId, int RowId, int SeatId, SeatStatusNow Status, PriceOptionNow Price);

/// <summary>
/// A status of the seat.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record SeatStatusNow(int Id, string Name);

/// <summary>
/// A seat price.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
public record PriceOptionNow(int Id, string Name);
