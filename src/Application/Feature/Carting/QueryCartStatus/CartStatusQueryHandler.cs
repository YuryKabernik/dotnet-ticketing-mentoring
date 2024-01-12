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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<CartStatusQueryResponse> Handle(CartStatusQuery query, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(query.CartId, cancellation);

        if (cart is null)
            throw new NotFoundException($"Cart {query.CartId} was not found.");

        return new CartStatusQueryResponse(query.CartId, cart.Seats);
    }
}
