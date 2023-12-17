using Ticketing.Domain.Entities;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface ICartRepository : IRepository<Cart, Guid>
{
    Task<Cart?> GetWithSeatsAsync(Guid cartId, CancellationToken cancellation);
}
