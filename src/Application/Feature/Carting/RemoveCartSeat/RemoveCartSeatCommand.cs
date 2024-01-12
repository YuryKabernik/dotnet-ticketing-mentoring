using MediatR;

namespace Ticketing.Application.Feature.Carting.RemoveCartSeat;

public record RemoveCartSeatCommand(Guid CartId, int EventId, int SeatId) : IRequest;
