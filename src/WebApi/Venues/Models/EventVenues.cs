namespace Ticketing.WebApi.Models;

public record EventVenues(IEnumerable<EventVenue> Venues);

public record EventVenue(string Name, string Address);
