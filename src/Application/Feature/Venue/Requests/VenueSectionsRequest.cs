using MediatR;

namespace Ticketing.Application.Feature.Venue.Requests;

public record VenueSectionsRequest(int VenueId) : IRequest<VenueSectionsResponse>;
