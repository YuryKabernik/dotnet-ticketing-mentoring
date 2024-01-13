namespace Ticketing.Application.Feature.Event.Response;

public record EventSeatsResponse(IEnumerable<EventSeatDetails> Seats);
