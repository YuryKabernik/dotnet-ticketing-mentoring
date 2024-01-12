using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Venue.Requests;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Venue;

public record VenueSectionsResponse(IEnumerable<Section> Sections);

/// <summary>
/// Query all sections for the venue.
/// </summary>
public class VenueSectionsQueryHandler : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IVenueRepository _venueRepository;

    public VenueSectionsQueryHandler(IVenueRepository venueRepository)
    {
        this._venueRepository = venueRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<VenueSectionsResponse> Handle(VenueSectionsRequest request, CancellationToken cancellationToken)
    {
        var venue = await this._venueRepository.GetWithSectionsAsync(request.VenueId, cancellationToken);

        if (venue is null)
            throw new NotFoundException($"Venue {request.VenueId} was not found.");

        return new VenueSectionsResponse(venue.Sections);
    }
}
