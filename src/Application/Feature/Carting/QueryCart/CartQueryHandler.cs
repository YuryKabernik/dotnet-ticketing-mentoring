using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public class CartQueryHandler : IQueryHandler<CartQuery, CartQueryResponse>
{
    private readonly ICartRepository _cartRepository;

    public CartQueryHandler(ICartRepository cartRepository)
    {
        this._cartRepository = cartRepository;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<CartQueryResponse> Handle(CartQuery query, CancellationToken cancellationToken)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(query.CartId, cancellationToken);

        if (cart is null)
            throw new NotFoundException($"Cart {query.CartId} is not found.");

        return new CartQueryResponse(cart.Guid, cart.Seats);
    }
}
