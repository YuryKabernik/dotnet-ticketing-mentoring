using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Application.ObjectMapping;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Event;

public record EventSeatsBySectionRequest(int EventId, int SectionId) : IRequest<EventSeatsResponse>;

public record EventSeatsResponse(IEnumerable<EventSeatDetails> EventSeats);

/// <summary>
/// Returns the list of seats (section_id, row_id, seat_id)
/// with seats’ status (id, name) and price options (id, name)
/// </summary>
public class EventSeatsBySectionQuery : IQueryHandler<EventSeatsBySectionRequest, EventSeatsResponse>
{
    private readonly IEventSeatRepository _eventSeatRepository;

    public EventSeatsBySectionQuery(IEventSeatRepository eventSeatRepository)
    {
        this._eventSeatRepository = eventSeatRepository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<EventSeatsResponse> Handle(EventSeatsBySectionRequest request, CancellationToken cancellationToken) =>
        this.Handle(request, cancellationToken);

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<EventSeatsResponse> ExecuteAsync(
        EventSeatsBySectionRequest request,
        CancellationToken cancellation)
    {
        var seats = await this._eventSeatRepository.GetBySectionWithOrderPriceAsync(
            request.EventId,
            request.SectionId,
            cancellation
        );

        var seatDetails = seats.Select(EventSeatMapper.ToDetailedResponse);

        return new EventSeatsResponse(seatDetails);
    }
}