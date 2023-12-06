using Microsoft.EntityFrameworkCore;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess;

public class VenueQuery : IRepository<Venue>
{
    private readonly DataContext context;

    public VenueQuery(DataContext context)
    {
        this.context = context;
    }

    public async Task<Venue?> FirstAsync(int venueId)
    {
        return await this.context.Venues.SingleAsync(venue => venue.Id == venueId);
    }

    public async Task<IEnumerable<Venue>?> ListAsync()
    {
        return await this.context.Venues.ToListAsync();
    }
}
