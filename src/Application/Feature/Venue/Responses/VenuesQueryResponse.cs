namespace Ticketing.Application.Feature.Venue.Responses;

public record VenuesQueryResponse(IEnumerable<Domain.Entities.Venue.Venue> Venues);