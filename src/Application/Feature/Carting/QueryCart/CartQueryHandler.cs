using Ticketing.Application.CQRS;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.QueryCart;

public class CartQueryHandler : IQueryHandler<CartQuery, CartQueryResponse>
{
    private readonly ICartRepository _repository;

    public CartQueryHandler(ICartRepository repository)
    {
        this._repository = repository;
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
        var result = await this._repository.GetWithSeatsAsync(query.CartId, cancellation);
        NotFoundException.ThrowIfNull(result!);

        return new(
            result!.Guid,
            result!.Seats
        );
    }

}
