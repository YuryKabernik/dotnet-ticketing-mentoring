using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.Feature.Venue.Requests;
using Ticketing.Application.Feature.Venue.Responses;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Venue resources.
/// </summary>
[ApiController]
[Route("api/venues")]
public class VenueController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Returns all venues
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<EventVenues>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetVenues(CancellationToken cancellationToken)
    {
        VenuesQueryRequest request = new();
        VenuesQueryResponse response = await mediator.Send(request, cancellationToken);

        return Results.Ok(response.ToEventVenues());
    }

    /// <summary>
    /// Returns all sections for the venue.
    /// </summary>
    /// <param name="venueId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{venueId:int}/sections")]
    [ProducesResponseType<VenueSections>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetVenues(int venueId, CancellationToken cancellationToken)
    {
        VenueSectionsRequest request = new(venueId);
        VenueSectionsResponse response = await mediator.Send(request, cancellationToken);

        return TypedResults.Ok(response.ToVenueSections());
    }
}
