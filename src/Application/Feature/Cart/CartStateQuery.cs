using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Application.Feature.Cart.Response;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Cart;

public class CartStateQuery : IQueryHandler<CartRequest, CartStateResponse>
{
    private readonly ICartRepository cartRepository;

    public CartStateQuery(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<CartStateResponse> ExecuteAsync(CartRequest request, CancellationToken cancellation)
    {
        var cart = await this.cartRepository.GetWithSeatsAsync(request.CartId, cancellation);

        return new CartStateResponse(request.CartId, cart!.Seats);
    }
}
