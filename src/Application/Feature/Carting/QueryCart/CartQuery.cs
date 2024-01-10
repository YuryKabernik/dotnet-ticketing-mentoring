using MediatR;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public record CartQuery(Guid CartId) : IRequest<CartQueryResponse>;
