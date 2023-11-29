using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Events.Models;

namespace Ticketing.WebApi.Events;

/// <summary>
/// Describes Event resources.
/// </summary>
[Route("events")]
[ApiController]
public class EventController : ControllerBase
{
    private IEnumerable<OrganizedEvent> Events { get; set; }
    private IEnumerable<EventSeat> Seats { get; set; }

    /// <summary>
    /// Initializes DI services.
    /// </summary>
    public EventController()
    {
        this.Events = new List<OrganizedEvent>();
        this.Seats = new List<EventSeat>();
    }

    /// <summary>
    /// Returns a list of events available for booking.
    /// </summary>
    /// <returns>
    ///     A list of available events.
    /// </returns>
    [HttpGet]
    public Task<IEnumerable<OrganizedEvent>> GetEvents()
    {
        return Task.FromResult(this.Events);
    }

    /// <summary>
    /// Returns a list of seats with seats’ status and price options.
    /// </summary>
    /// <param name="eventId">Dedicated event to search within.</param>
    /// <param name="sectionId">Dedicated section to search within.</param>
    /// <returns>
    ///     A list of seats with seats’ status and price options.
    /// </returns>
    [HttpGet]
    [Route("{eventId:int}/sections/{sectionId:int}/seats")]
    public Task<IEnumerable<EventSeat>> GetSeats(int eventId, int sectionId)
    {
        return Task.FromResult(this.Seats);
    }
}
