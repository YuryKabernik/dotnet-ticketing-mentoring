using MediatR;
using Ticketing.Application.Feature.Venue.Responses;

namespace Ticketing.Application.Feature.Venue.Requests;

public record VenuesQueryRequest() : IRequest<VenuesQueryResponse>;
