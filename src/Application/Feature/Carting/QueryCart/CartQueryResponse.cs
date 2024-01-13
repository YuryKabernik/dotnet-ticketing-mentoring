using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public record CartQueryResponse(Guid CartId, ICollection<EventSeat> Seats);
