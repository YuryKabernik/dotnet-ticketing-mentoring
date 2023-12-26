namespace Ticketing.Application.Feature.Carting.UpdateCartSeats;

public record UpdateCartCommand(Guid CartId, SeatPayload Payload);
public record SeatPayload(int EventId, int SeatId, int PriceId);
