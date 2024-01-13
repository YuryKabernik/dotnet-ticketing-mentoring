using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Event.Requests;
using Ticketing.Application.Feature.Event.Response;
using Ticketing.Application.ObjectMapping;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Event;

/// <summary>
/// Returns the list of seats (section_id, row_id, seat_id)
/// with seats’ status (id, name) and price options (id, name)
/// </summary>
public class EventSeatsBySectionQueryHandler : IQueryHandler<EventSeatsBySectionRequest, EventSeatsResponse>
{
    private readonly IEventSeatRepository _eventSeatRepository;

    public EventSeatsBySectionQueryHandler(IEventSeatRepository eventSeatRepository)
    {
        this._eventSeatRepository = eventSeatRepository;
    }

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<EventSeatsResponse> Handle(EventSeatsBySectionRequest request,
        CancellationToken cancellationToken)
    {
        var seats = await this._eventSeatRepository.GetBySectionWithOrderPriceAsync(
            request.EventId,
            request.SectionId,
            cancellationToken
        );

        var seatDetails = seats.Select(EventSeatMapper.ToDetailedResponse);

        return new EventSeatsResponse(seatDetails);
    }
}