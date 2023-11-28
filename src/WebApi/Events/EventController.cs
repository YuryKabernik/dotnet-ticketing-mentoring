using Microsoft.AspNetCore.Mvc;
using Ticketing.WebApi.Events.Models;

namespace Ticketing.WebApi.Events;

[Route("events")]
[ApiController]
public class EventController : ControllerBase
{
    public IEnumerable<OrganizedEvent> Events { get; set; }
    public IEnumerable<EventSeat> Seats { get; set; }

    public EventController()
    {
        this.Events = new List<OrganizedEvent>();
        this.Seats = new List<EventSeat>();
    }

    [HttpGet]
    public Task<IEnumerable<OrganizedEvent>> GetEvents()
    {
        return Task.FromResult(this.Events);
    }

    [HttpGet]
    [Route("{eventId:int}/sections/{sectionId:int}/seats")]
    public Task<IEnumerable<EventSeat>> GetSeats(int eventId, int sectionId)
    {
        return Task.FromResult(this.Seats);
    }
}
