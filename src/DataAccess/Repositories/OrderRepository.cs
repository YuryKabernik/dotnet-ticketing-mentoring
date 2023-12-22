using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext context;

    public OrderRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task Add(Order order, CancellationToken cancellation)
    {
        await this.context.AddAsync(order, cancellation);
    }

    public async Task<Order?> GetAsync(int id, CancellationToken cancellation)
    {
        return await this.context.Orders
            .Include(order => order.Seats)
            .Include(order => order.Status)
            .Include(order => order.Payment)
            .SingleOrDefaultAsync(order => order.Id == id, cancellation);
    }

    public async Task<IEnumerable<Order>?> ListAsync(CancellationToken cancellation)
    {
        return await this.context.Orders.ToListAsync(cancellation);
    }
}
