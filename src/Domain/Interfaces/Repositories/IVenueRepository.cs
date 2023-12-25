using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IVenueRepository : IRepository<Venue, int>
{
    Task<Venue> GetWithSectionsAsync(int venueId, CancellationToken cancellation);
}