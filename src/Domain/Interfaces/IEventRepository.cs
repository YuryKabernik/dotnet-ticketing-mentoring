using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces;

public interface IEventRepository : IRepository<Event, int>
{
    Task<IEnumerable<EventSeat>> GetSeatsAsync(int eventId, int sectionId, CancellationToken cancellation);
}
