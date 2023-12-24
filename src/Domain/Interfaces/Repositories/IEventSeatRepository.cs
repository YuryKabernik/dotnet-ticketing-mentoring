using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IEventSeatRepository
{
    Task<EventSeat> GetAsync(int eventId, CancellationToken cancellation);
    Task<IEnumerable<EventSeat>> GetBySectionWithOrderPriceAsync(int eventId, int sectionId, CancellationToken cancellation);
}
