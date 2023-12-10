using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces;

namespace Ticketing.Application;

public record VenueSectionsRequest(int VenueId);
public record VenueSectionsResponse(IEnumerable<Section> Sections);

/// <summary>
/// Query all sections for the venue.
/// </summary>
public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IRepository<Venue, int> repository;

    public VenueSectionsQuery(IRepository<Venue, int> repository)
    {
        this.repository = repository;
    }

    public async Task<VenueSectionsResponse> ExecuteAsync(VenueSectionsRequest request, CancellationToken cancellation)
    {
        var venue = await this.repository.FirstAsync(request.VenueId, cancellation);

        return new VenueSectionsResponse(venue?.Sections?.ToList()!);
    }
}
