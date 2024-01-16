using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Event.Response;
using Ticketing.Domain.Entities.Event;
using Ticketing.WebApi.Events.Models;

namespace Ticketing.WebApi.Events.Mappers;

[Mapper]
public static partial class EventsResponseMapper
{
    public static partial AvailableEvents ToAvailableEvents(this EventsResponse response);
    
    [MapProperty(nameof(Event.Id), nameof(OrganizedEvent.Id))]
    [MapProperty(nameof(Event.Name), nameof(OrganizedEvent.Name))]
    [MapProperty(nameof(Event.DateTime), nameof(OrganizedEvent.DateTime))]
    [MapProperty(nameof(@Event.Venue.Address), nameof(OrganizedEvent.Place))]
    private static partial OrganizedEvent ToOrganizedEvent(this Event response);
}
