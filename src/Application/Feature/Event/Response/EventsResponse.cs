namespace Ticketing.Application.Feature.Event.Response;

public record EventsResponse(IEnumerable<Domain.Entities.Event.Event> Events);
