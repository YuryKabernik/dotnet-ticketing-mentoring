using Ticketing.Application.CQRS;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Application;

public record VenueSectionsRequest(int VenueId);
public record VenueSectionsResponse(IEnumerable<Section> Sections);

/// <summary>
/// Query all sections for the venue.
/// </summary>
public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IRepository<Venue> repository;

    public VenueSectionsQuery(IRepository<Venue> repository)
    {
        this.repository = repository;
    }

    public async Task<VenueSectionsResponse> ExecuteAsync(VenueSectionsRequest request, CancellationToken cancellation)
    {
        var venue = await this.repository.FirstAsync(request.VenueId, cancellation);

        return new VenueSectionsResponse(venue?.Sections?.ToList()!);
    }
}
