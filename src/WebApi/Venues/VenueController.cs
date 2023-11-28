using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

[Route("venues")]
[ApiController]
public class VenueController : ControllerBase
{
    private readonly IEnumerable<EventVenue> Venues;
    private readonly IEnumerable<VenueSection> Sections;

    public VenueController()
    {
        this.Venues = new List<EventVenue>();
        this.Sections = new List<VenueSection>();
    }

    [HttpGet]
    public Task<IEnumerable<EventVenue>> GetVenues()
    {
        return Task.FromResult(this.Venues);
    }

    [HttpGet]
    [Route("{venueId:int}/sections")]
    public Task<IEnumerable<VenueSection>> GetVenues(int venueId)
    {
        return Task.FromResult(this.Sections);
    }
}
