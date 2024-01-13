using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Venue.Responses;
using Ticketing.Domain.Entities.Venue;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

[Mapper]
public static partial class EventVenueSectionsMapper
{
    [MapProperty(nameof(VenueSectionsResponse.Sections), nameof(VenueSections.Sections))]
    public static partial VenueSections ToVenueSections(this VenueSectionsResponse response);

    [MapProperty(nameof(Section.Label), nameof(VenueSection.Label))]
    public static partial VenueSection ToVenueSection(this Section response);
}
