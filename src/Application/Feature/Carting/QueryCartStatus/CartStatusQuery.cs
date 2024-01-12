using MediatR;

namespace Ticketing.Application.Feature.Carting.QueryCartStatus;

public record CartStatusQuery(Guid CartId) : IRequest<CartStatusQueryResponse>;