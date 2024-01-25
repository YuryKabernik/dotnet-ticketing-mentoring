using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.Feature.Event.Requests;
using Ticketing.Application.Feature.Event.Response;
using Ticketing.WebApi.Events.Mappers;
using Ticketing.WebApi.Events.Models;
using Ticketing.WebApi.Caching;

namespace Ticketing.WebApi.Events;

/// <summary>
/// Describes Event resources.
/// </summary>
[ApiController]
[Route("api/events")]
[ResponseCache(CacheProfileName = CacheProfileRegister.EventsProfileName)]
public class EventController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Returns a list of events available for booking.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A list of available events.
    /// </returns>
    [HttpGet]
    [ProducesResponseType<AvailableEvents>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetEvents(CancellationToken cancellationToken)
    {
        EventsRequest request = new();
        EventsResponse response = await mediator.Send(request, cancellationToken);

        return Results.Ok(response.ToAvailableEvents());
    }

    /// <summary>
    /// Returns a list of seats with seats’ status and price options.
    /// </summary>
    /// <param name="eventId">Dedicated event to search within.</param>
    /// <param name="sectionId">Dedicated section to search within.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     A list of seats with seats’ status and price options.
    /// </returns>
    [HttpGet("{eventId:int}/sections/{sectionId:int}/seats")]
    [ProducesResponseType<AvailableEventSeats>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetSeats(int eventId, int sectionId, CancellationToken cancellationToken)
    {
        EventSeatsBySectionRequest request = new(eventId, sectionId);
        EventSeatsResponse response = await mediator.Send(request, cancellationToken);

        return Results.Ok(response.ToAvailableEventSeats());
    }
}
