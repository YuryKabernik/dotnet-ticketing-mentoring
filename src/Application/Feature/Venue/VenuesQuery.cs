using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Venue.Requests;
using Ticketing.Application.Feature.Venue.Responses;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Venue;

public class VenuesQuery : IQueryHandler<VenuesQueryRequest, VenuesQueryResponse>
{
    private readonly IVenueRepository _venueRepository;

    public VenuesQuery(IVenueRepository venueRepository)
    {
        this._venueRepository = venueRepository;
    }

    public async Task<VenuesQueryResponse> ExecuteAsync(VenuesQueryRequest request, CancellationToken cancellation)
    {
        var result = await this._venueRepository.ListAsync(cancellation);

        return new VenuesQueryResponse(result!);
    }
}
