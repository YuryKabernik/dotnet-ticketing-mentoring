using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

public record CartRequest(Guid CartId);
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
        var result = await this.repository.GetAsync(request.CartId, cancellation);

        return new(
            result!.Guid,
            result!.Seats
        );
    }
}
