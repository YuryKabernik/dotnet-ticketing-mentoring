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
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<CartQueryResponse> Handle(CartQuery request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<CartQueryResponse> ExecuteAsync(CartQuery query, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(query.CartId, cancellation);

        if (cart is null)
            throw new NotFoundException($"Cart {query.CartId} is not found.");
        
        return new(
            cart.Guid,
            cart.Seats
        );
    }

}
