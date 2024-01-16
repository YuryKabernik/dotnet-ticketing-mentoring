using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Event.Response;
using Ticketing.WebApi.Events.Models;

namespace Ticketing.WebApi.Events.Mappers;

[Mapper]
public static partial class EventSeatsResponseMapper
{
    public static partial AvailableEventSeats ToAvailableEventSeats(this EventSeatsResponse response);
}
