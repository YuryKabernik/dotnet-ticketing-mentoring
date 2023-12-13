using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces;

namespace Ticketing.DataAccess;

public class EventSeatRepository : IEventSeatRepository
{
    private readonly DataContext context;

    public EventSeatRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<EventSeat?> FirstAsync(int seatId, CancellationToken cancellation)
    {
        return await this.context.EventSeats.SingleOrDefaultAsync(seat => seat.Id == seatId, cancellation);
    }
}
