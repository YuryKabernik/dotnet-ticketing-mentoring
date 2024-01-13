using MediatR;
using Ticketing.Application.Feature.Event.Response;

namespace Ticketing.Application.Feature.Event.Requests;

public record EventSeatsBySectionRequest(int EventId, int SectionId) : IRequest<EventSeatsResponse>;
