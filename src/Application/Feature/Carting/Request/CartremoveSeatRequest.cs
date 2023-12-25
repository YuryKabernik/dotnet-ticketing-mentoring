namespace Ticketing.Application.Feature.Cart.Request;

public record CartRemoveSeatRequest(Guid CartId, int EventId, int SeatId);
