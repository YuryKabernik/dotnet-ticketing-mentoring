using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IEventSeatRepository
{
    Task<EventSeat?> GetAsync(int id, CancellationToken cancellation);
    Task<IEnumerable<EventSeat>> GetWithOrderAndPriceAsync(int eventId, int sectionId, CancellationToken cancellation);
}
