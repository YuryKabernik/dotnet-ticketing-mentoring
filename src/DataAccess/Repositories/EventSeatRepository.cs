using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess;

public class EventSeatRepository : IEventSeatRepository
{
    private readonly DataContext context;

    public EventSeatRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<EventSeat?> GetAsync(int id, CancellationToken cancellation)
    {
        return await this.context.EventSeats
            .Include(seat => seat.Row!.Section!.Event)
            .SingleOrDefaultAsync(seat => seat.Id == id, cancellation);
    }

    public async Task<IEnumerable<EventSeat>> GetWithOrderAndPriceAsync(
        int eventId,
        int sectionId,
        CancellationToken cancellation)
    {
        var seat = await this.context.EventSeats
            .Include(seat => seat.Price)
            .Include(seat => seat.Order)
            .Where(seat =>
                seat.Row!.Section!.Id == sectionId &&
                seat.Row!.Section!.Event!.Id == eventId
            )
            .ToListAsync();

        return seat;
    }
}
