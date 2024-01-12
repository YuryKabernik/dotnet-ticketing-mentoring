using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Venue.Requests;
using Ticketing.Application.Feature.Venue.Responses;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Venue;

public class VenuesQueryHandler : IQueryHandler<VenuesQueryRequest, VenuesQueryResponse>
{
    private readonly IVenueRepository _venueRepository;

    public VenuesQueryHandler(IVenueRepository venueRepository)
    {
        this._venueRepository = venueRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<VenuesQueryResponse> Handle(VenuesQueryRequest request, CancellationToken cancellationToken)

    {
        var venues = await this._venueRepository.ListAsync(cancellationToken);

        return new VenuesQueryResponse(venues);
    }
}
