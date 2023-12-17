using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Cart;

public record CartResponse(Guid CartId, IEnumerable<EventSeat> Seats);

public class CartQuery : IQueryHandler<CartRequest, CartResponse>
{
    private readonly ICartRepository repository;

    public CartQuery(ICartRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CartResponse> ExecuteAsync(CartRequest request, CancellationToken cancellation)
    {
        var result = await this.repository.GetWithSeatsAsync(request.CartId, cancellation);

        return new(
            result!.Guid,
            result!.Seats
        );
    }
}
