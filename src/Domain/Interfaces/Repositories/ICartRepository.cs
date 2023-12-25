using Ticketing.Domain.Entities;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetAsync(Guid id, CancellationToken cancellation);
    Task<Cart?> GetWithSeatsAsync(Guid cartId, CancellationToken cancellation);
    Task<Cart?> GetWithSeatsEventsAsync(Guid cartId, CancellationToken cancellation);
}