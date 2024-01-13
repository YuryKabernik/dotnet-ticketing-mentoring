using MediatR;
using Ticketing.Application.Feature.Event.Response;

namespace Ticketing.Application.Feature.Event.Requests;

public record EventsRequest() : IRequest<EventsResponse>;
