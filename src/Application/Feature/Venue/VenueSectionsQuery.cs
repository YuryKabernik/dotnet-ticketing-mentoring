using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

public record VenueSectionsRequest(int VenueId);
public record VenueSectionsResponse(IEnumerable<Section> Sections);

/// <summary>
/// Query all sections for the venue.
/// </summary>
public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IVenueRepository repository;

    public VenueSectionsQuery(IVenueRepository repository)
    {
        this.repository = repository;
    }

    public async Task<VenueSectionsResponse> ExecuteAsync(VenueSectionsRequest request, CancellationToken cancellation)
    {
        var venue = await this.repository.GetAsync(request.VenueId, cancellation);

        return new VenueSectionsResponse(venue?.Sections?.ToList()!);
    }
}
