using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces;

public interface IEventSeatRepository
{
    Task<EventSeat?> FirstAsync(int seatId, CancellationToken cancellation);
}
