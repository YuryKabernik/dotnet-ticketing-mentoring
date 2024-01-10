using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Application.ObjectMapping;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Event;

public record EventSeatsBySectionRequest(int EventId, int SectionId) : IRequest<EventSeatsBySectionResponse>;
public record EventSeatsBySectionResponse(IEnumerable<EventSeatDetails> EventSeats);

/// <summary>
/// Returns the list of seats (section_id, row_id, seat_id)
/// with seats’ status (id, name) and price options (id, name)
/// </summary>
public class EventSeatsBySectionQuery : IQueryHandler<EventSeatsBySectionRequest, EventSeatsBySectionResponse>
{
    private readonly IEventSeatRepository repository;

    public EventSeatsBySectionQuery(IEventSeatRepository repository)
    {
        this.repository = repository;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<EventSeatsBySectionResponse> Handle(EventSeatsBySectionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Application implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<EventSeatsBySectionResponse> ExecuteAsync(
        EventSeatsBySectionRequest request,
        CancellationToken cancellation)
    {
        var seats = await this.repository.GetBySectionWithOrderPriceAsync(request.EventId, request.SectionId, cancellation);
        var seatDetails = seats.Select(EventSeatMapper.ToDetailedResponse);

        return new EventSeatsBySectionResponse(seatDetails);
    }
}
