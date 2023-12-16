using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IEventRepository : IRepository<Event, int>
{
}
