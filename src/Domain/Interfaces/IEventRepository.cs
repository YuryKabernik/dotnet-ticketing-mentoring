using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<EventSeat>> GetSeatsAsync(int eventId, int sectionId, CancellationToken cancellation);
}
