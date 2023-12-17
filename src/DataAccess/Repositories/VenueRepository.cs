using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly DataContext context;

    public VenueRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<Venue?> GetAsync(int venueId, CancellationToken cancellation)
    {
        return await this.context.Venues.SingleOrDefaultAsync(venue => venue.Id == venueId, cancellation);
    }

    public async Task<IEnumerable<Venue>?> ListAsync(CancellationToken cancellation)
    {
        return await this.context.Venues.ToListAsync(cancellation);
    }
}
