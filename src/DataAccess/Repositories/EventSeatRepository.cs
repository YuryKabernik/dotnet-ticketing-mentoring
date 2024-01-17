using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class EventSeatRepository : IEventSeatRepository
{
    private readonly DataContext _context;

    public EventSeatRepository(DataContext context)
    {
        this._context = context;
    }

    public async Task<EventSeat?> GetWithPriceEventAsync(int eventId, CancellationToken cancellation)
    {
        return await this._context.EventSeats
            .Include(seat => seat.Price)
            .Include(seat => seat.Row!.Section!.Event)
            .SingleOrDefaultAsync(seat => seat.Id == eventId, cancellation);
    }

    public async Task<IEnumerable<EventSeat>> GetBySectionWithOrderPriceAsync(
        int eventId,
        int sectionId,
        CancellationToken cancellation)
    {
        var seat = await this._context.EventSeats
            .Include(seat => seat.Price)
            .Include(seat => seat.Order)
            .Where(seat =>
                seat.Row!.Section!.Id == sectionId &&
                seat.Row!.Section!.Event!.Id == eventId
            )
            .ToListAsync(cancellation);

        return seat;
    }
}
