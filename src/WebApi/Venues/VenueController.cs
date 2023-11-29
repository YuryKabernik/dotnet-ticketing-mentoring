using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

/// <summary>
/// Describes Venue resources.
/// </summary>
[Route("venues")]
[ApiController]
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
    public Task<IEnumerable<EventVenue>> GetVenues()
    {
        return Task.FromResult(this.Venues);
    }

    /// <summary>
    /// Returns all sections for the venue.
    /// </summary>
    /// <param name="venueId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{venueId:int}/sections")]
    public Task<IEnumerable<VenueSection>> GetVenues(int venueId)
    {
        return Task.FromResult(this.Sections);
    }
}
