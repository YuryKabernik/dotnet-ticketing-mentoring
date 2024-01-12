using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Venue resources.
/// </summary>
[ApiController]
[Route("api/venues")]
public class VenueController : ControllerBase
{
    private readonly IEnumerable<EventVenue> Venues;
    private readonly IEnumerable<VenueSection> Sections;

    /// <summary>
    /// Initializes DI services.
    /// </summary>
    public VenueController()
    {
        this.Venues = new List<EventVenue>();
        this.Sections = new List<VenueSection>();
    }

    /// <summary>
    /// Returns all venues
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<EventVenues>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult GetVenues()
    {
        return TypedResults.Ok<EventVenues>(
            new(this.Venues)
        );
    }

    /// <summary>
    /// Returns all sections for the venue.
    /// </summary>
    /// <param name="venueId"></param>
    /// <returns></returns>
    [HttpGet("{venueId:int}/sections")]
    [ProducesResponseType<VenueSections>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult GetVenues(int venueId)
    {
        return TypedResults.Ok<VenueSections>(
            new(this.Sections)
        );
    }
}
