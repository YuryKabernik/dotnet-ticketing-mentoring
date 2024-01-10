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
    public async Task<CartStatusQueryResponse> ExecuteAsync(CartStatusQuery query, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(query.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        return new CartStatusQueryResponse(query.CartId, cart!.Seats);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<CartStatusQueryResponse> Handle(CartStatusQuery request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);
}
