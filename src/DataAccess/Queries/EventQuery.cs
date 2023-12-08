using Microsoft.EntityFrameworkCore;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.DataAccess;

public class EventQuery : IRepository<Event>
{
    private readonly DataContext context;

    public EventQuery(DataContext context)
    {
        this.context = context;
    }

    public async Task<Event?> FirstAsync(int id)
    {
        return await this.context.Events.SingleAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>?> ListAsync()
    {
        return await this.context.Events.ToArrayAsync();
    }
}
