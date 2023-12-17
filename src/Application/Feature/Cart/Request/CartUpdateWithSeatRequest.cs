namespace Ticketing.Application.Feature.Cart.Request;

public record CartUpdateWithSeatRequest(Guid CartId, SeatPayload Payload);
public record SeatPayload(int EventId, int SeatId, int PriceId);
