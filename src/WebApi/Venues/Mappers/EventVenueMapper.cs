using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Venue.Responses;
using Ticketing.Domain.Entities.Venue;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

[Mapper]
public static partial class EventVenueMapper
{
    [MapProperty(nameof(VenuesQueryResponse.Venues), nameof(EventVenues.Venues))]
    public static partial EventVenues ToEventVenues(this VenuesQueryResponse response);

    [MapProperty(nameof(Venue.Name), nameof(EventVenue.Name))]
    [MapProperty(nameof(Venue.Address), nameof(EventVenue.Address))]
    public static partial EventVenue ToEventVenue(this Venue venue);
}
