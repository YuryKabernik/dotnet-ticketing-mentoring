using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess.Repositories;

public class VenueRepository : IRepository<Venue, int>
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
