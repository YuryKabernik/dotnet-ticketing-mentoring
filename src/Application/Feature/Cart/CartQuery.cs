using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces;

namespace Ticketing.Application;

public record CartRequest(Guid CartId);
public record CartResponse(Guid CartId, IEnumerable<EventSeat> Seats);

public class CartQuery : IQueryHandler<CartRequest, CartResponse>
{
    private readonly IRepository<Cart, Guid> repository;

    public CartQuery(IRepository<Cart, Guid> repository)
    {
        this.repository = repository;
    }

    public async Task<CartResponse> ExecuteAsync(CartRequest request, CancellationToken cancellation)
    {
        var result = await this.repository.FirstAsync(request.CartId, cancellation);

        return new(
            result!.Guid,
            result!.Seats
        );
    }
}
