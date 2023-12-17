using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DataContext context;

    public CartRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<Cart?> GetWithSeatsAsync(Guid cartId, CancellationToken cancellation)
    {
        return await this.context.Carts
            .Include(cart => cart.Seats)
            .ThenInclude(seat => seat.Price)
            .SingleOrDefaultAsync(e => e.Guid == cartId, cancellation);
    }

    public async Task<Cart?> GetWithSeatsEventsAsync(Guid cartId, CancellationToken cancellation)
    {
        return await this.context.Carts
            .Include(cart => cart.Seats)
            .ThenInclude(seat => seat.Row!.Section!.Event)
            .SingleOrDefaultAsync(e => e.Guid == cartId, cancellation);
    }

    public async Task<Cart?> GetAsync(Guid cartId, CancellationToken cancellation)
    {
        return await this.context.Carts.SingleOrDefaultAsync(e => e.Guid == cartId, cancellation);
    }

    public async Task<IEnumerable<Cart>?> ListAsync(CancellationToken cancellation)
    {
        return await this.context.Carts.ToListAsync(cancellation);
    }
}
