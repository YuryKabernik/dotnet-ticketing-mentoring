using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public record CartQueryResponse(Guid CartId, IEnumerable<EventSeat> Seats);
