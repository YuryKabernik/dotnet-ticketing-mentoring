﻿using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Event;

public record EventsRequest() : IRequest<EventsResponse>;

public record EventsResponse(IEnumerable<Domain.Entities.Event.Event> Events);

public class EventsQueryHandler : IQueryHandler<EventsRequest, EventsResponse>
{
    private readonly IEventRepository _eventRepository;

    public EventsQueryHandler(IEventRepository eventRepository)
    {
        this._eventRepository = eventRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<EventsResponse> Handle(EventsRequest request, CancellationToken cancellationToken)
    {
        var events = await this._eventRepository.ListAsync(cancellationToken);

        if (events is null)
            throw new NotFoundException($"A list of Events was not found");
        
        return new EventsResponse(events);
    }
}
