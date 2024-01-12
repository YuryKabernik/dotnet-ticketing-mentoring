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
public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IVenueRepository _venueRepository;

    public VenueSectionsQuery(IVenueRepository venueRepository)
    {
        this._venueRepository = venueRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<VenueSectionsResponse> Handle(VenueSectionsRequest request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<VenueSectionsResponse> ExecuteAsync(VenueSectionsRequest request, CancellationToken cancellation)
    {
        var venue = await this._venueRepository.GetWithSectionsAsync(request.VenueId, cancellation);

        if (venue is null)
            throw new NotFoundException($"Venue {request.VenueId} was not found.");

        return new VenueSectionsResponse(venue.Sections);
    }
}
