using Ticketing.Application.CQRS;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Application;

public record VenuesQueryRequest();

public record VenuesQueryResponse(IEnumerable<Venue> Venues);

public class VenuesQuery : IQueryHandler<VenuesQueryRequest, VenuesQueryResponse>
{
    private readonly IRepository<Venue> repository;

    public VenuesQuery(IRepository<Venue> repository)
    {
        this.repository = repository;
    }

    public async Task<VenuesQueryResponse> ExecuteAsync(VenuesQueryRequest request, CancellationToken cancellation)
    {
        var result = await this.repository.ListAsync(cancellation);

        return new VenuesQueryResponse(result!);
    }
}
