using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Event;

public record EventsRequest() : IRequest<EventsResponse>;

public record EventsResponse(IEnumerable<Domain.Entities.Event.Event> Events);

public class EventsQuery : IQueryHandler<EventsRequest, EventsResponse>
{
    private readonly IEventRepository _eventRepository;

    public EventsQuery(IEventRepository eventRepository)
    {
        this._eventRepository = eventRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<EventsResponse> ExecuteAsync(EventsRequest request, CancellationToken cancellation)
    {
        var events = await this._eventRepository.ListAsync(cancellation);
        
        return new EventsResponse(events);
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
