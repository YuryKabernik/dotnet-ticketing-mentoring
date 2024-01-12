using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Application.Feature.Venue.Responses;

public record VenueSectionsResponse(IEnumerable<Section> Sections);
