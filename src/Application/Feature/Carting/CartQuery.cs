using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting;

public record CartResponse(Guid CartId, IEnumerable<EventSeat> Seats);

public class CartQuery : IQueryHandler<CartRequest, CartResponse>
{
    private readonly ICartRepository _repository;

    public CartQuery(ICartRepository repository)
    {
        this._repository = repository;
    }

    public async Task<CartResponse> ExecuteAsync(CartRequest request, CancellationToken cancellation)
    {
        var result = await this._repository.GetWithSeatsAsync(request.CartId, cancellation);
        NotFoundException.ThrowIfNull(result!);
        
        return new(
            result!.Guid,
            result!.Seats
        );
    }
}
