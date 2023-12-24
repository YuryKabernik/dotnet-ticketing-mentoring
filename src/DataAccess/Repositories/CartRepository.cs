using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DataContext _context;

    public CartRepository(DataContext context)
    {
        this._context = context;
    }

    public Task<Cart> GetWithSeatsAsync(Guid cartId, CancellationToken cancellation)
    {
        return this._context.Carts
            .Include(cart => cart.Seats)
            .ThenInclude(seat => seat.Price)
            .SingleAsync(e => e.Guid == cartId, cancellation);
    }

    public Task<Cart> GetWithSeatsEventsAsync(Guid cartId, CancellationToken cancellation)
    {
        return this._context.Carts
            .Include(cart => cart.Seats)
            .ThenInclude(seat => seat.Row!)
            .ThenInclude(row => row.Section!)
            .ThenInclude(section => section.Event)
            .SingleAsync(e => e.Guid == cartId, cancellation);
    }

    public Task<Cart> GetAsync(Guid cartId, CancellationToken cancellation)
    {
        return this._context.Carts.SingleAsync(e => e.Guid == cartId, cancellation);
    }

    public async Task<IEnumerable<Cart>> ListAsync(CancellationToken cancellation)
    {
        return await this._context.Carts.ToListAsync(cancellation);
    }
}