using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

public record EventsRequest() : IRequest<EventsResponse>;

public record EventsResponse(IEnumerable<Event> Events);

public class EventsQuery : IQueryHandler<EventsRequest, EventsResponse>
{
    private readonly IEventRepository repository;

    public EventsQuery(IEventRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<EventsResponse> ExecuteAsync(EventsRequest request, CancellationToken cancellation)
    {
        var result = await this.repository.ListAsync(cancellation);

        return new EventsResponse(result!);
    }

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<EventsResponse> Handle(EventsRequest request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);
}
