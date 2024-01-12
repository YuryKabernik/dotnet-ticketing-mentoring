using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        this._context = context;
    }

    public async Task Add(Order order, CancellationToken cancellation)
    {
        await this._context.AddAsync(order, cancellation);
    }

    public async Task<Order> GetAsync(int id, CancellationToken cancellation)
    {
        return await this._context.Orders
            .Include(order => order.Seats)
            .Include(order => order.Status)
            .Include(order => order.Payment)
            .SingleAsync(order => order.Id == id, cancellation);
    }

    public async Task<IEnumerable<Order>> ListAsync(CancellationToken cancellation)
    {
        return await this._context.Orders.ToListAsync(cancellation);
    }
}