using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Venue.Requests;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Venue;

public record VenueSectionsResponse(IEnumerable<Section> Sections);

/// <summary>
/// Query all sections for the venue.
/// </summary>
public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IVenueRepository _venueRepository;

    public VenueSectionsQuery(IVenueRepository venueRepository)
    {
        this._venueRepository = venueRepository;
    }

    public async Task<VenueSectionsResponse> ExecuteAsync(VenueSectionsRequest request, CancellationToken cancellation)
    {
        var venue = await this._venueRepository.GetWithSectionsAsync(request.VenueId, cancellation);

        return new VenueSectionsResponse(venue?.Sections!);
    }
}
