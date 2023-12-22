using Ticketing.Domain.Entities.Ordering;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order, int>
{
    Task Add(Order order, CancellationToken cancellation);
}
