using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.QueryCartStatus;

public class CartStatusQueryHandler : IQueryHandler<CartStatusQuery, CartStatusQueryResponse>
{
    private readonly ICartRepository _cartRepository;

    public CartStatusQueryHandler(ICartRepository cartRepository)
    {
        this._cartRepository = cartRepository;
    }

    public async Task<CartStatusQueryResponse> ExecuteAsync(CartStatusQuery query, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(query.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        return new CartStatusQueryResponse(query.CartId, cart!.Seats);
    }
}
