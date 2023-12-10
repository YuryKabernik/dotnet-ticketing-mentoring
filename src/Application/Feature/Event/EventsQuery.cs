using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces;

namespace Ticketing.Application;

public record EventsRequest();
public record EventsResponse(IEnumerable<Event> Events);

public class EventsQuery : IQueryHandler<EventsRequest, EventsResponse>
{
    private readonly IEventRepository repository;

    public EventsQuery(IEventRepository repository)
    {
        this.repository = repository;
    }

    public async Task<EventsResponse> ExecuteAsync(EventsRequest request, CancellationToken cancellation)
    {
        var result = await this.repository.ListAsync(cancellation);

        return new EventsResponse(result!);
    }
}
