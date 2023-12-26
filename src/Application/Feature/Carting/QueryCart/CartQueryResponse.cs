using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public record CartResponse(Guid CartId, IEnumerable<EventSeat> Seats);
