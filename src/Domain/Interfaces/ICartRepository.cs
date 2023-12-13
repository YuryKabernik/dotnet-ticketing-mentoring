using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart, Guid>
{
    Task AddSeat(Guid cartId, EventSeat seat, CancellationToken cancellation);
}
