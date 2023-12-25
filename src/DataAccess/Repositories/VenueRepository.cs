using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly DataContext _context;

    public VenueRepository(DataContext context)
    {
        this._context = context;
    }

    public Task<Venue> GetAsync(int venueId, CancellationToken cancellation)
    {
        return this._context.Venues.SingleAsync(venue => venue.Id == venueId, cancellation);
    }

    public Task<Venue> GetWithSectionsAsync(int venueId, CancellationToken cancellation)
    {
        return this._context.Venues
            .Include(venue => venue.Sections)
            .SingleAsync(venue => venue.Id == venueId, cancellation);
    }

    public async Task<IEnumerable<Venue>> ListAsync(CancellationToken cancellation)
    {
        return await this._context.Venues.ToListAsync(cancellation);
    }
}
