using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Application.Feature.Cart.Response;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting;

public class CartStateQuery : IQueryHandler<CartRequest, CartStateResponse>
{
    private readonly ICartRepository _cartRepository;

    public CartStateQuery(ICartRepository cartRepository)
    {
        this._cartRepository = cartRepository;
    }

    public async Task<CartStateResponse> ExecuteAsync(CartRequest request, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(request.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        return new CartStateResponse(request.CartId, cart!.Seats);
    }
}
