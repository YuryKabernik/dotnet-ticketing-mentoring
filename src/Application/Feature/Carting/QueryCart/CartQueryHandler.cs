using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public class CartQueryHandler : IQueryHandler<CartQuery, CartResponse>
{
    private readonly ICartRepository _repository;

    public CartQueryHandler(ICartRepository repository)
    {
        this._repository = repository;
    }

    public async Task<CartResponse> ExecuteAsync(CartQuery query, CancellationToken cancellation)
    {
        var result = await this._repository.GetWithSeatsAsync(query.CartId, cancellation);
        NotFoundException.ThrowIfNull(result!);
        
        return new(
            result!.Guid,
            result!.Seats
        );
    }
}
