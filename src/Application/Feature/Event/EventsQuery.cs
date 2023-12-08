using Ticketing.Application.CQRS;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application;

public record EventsRequest();
public record EventsResponse(IEnumerable<Event> Events);

public class EventsQuery : IQueryHandler<EventsRequest, EventsResponse>
{
    private readonly IRepository<Event> repository;

    public EventsQuery(IRepository<Event> repository)
    {
        this.repository = repository;
    }

    public async Task<EventsResponse> Execute(EventsRequest request)
    {
        var result = await this.repository.ListAsync();

        return new EventsResponse(result!);
    }
}
