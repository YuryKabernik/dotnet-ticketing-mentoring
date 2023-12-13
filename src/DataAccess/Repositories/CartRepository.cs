using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Interfaces;

namespace Ticketing.DataAccess.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DataContext context;

    public CartRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task AddSeat(Guid cartId, EventSeat seat, CancellationToken cancellation)
    {
        var cart = await this.FirstAsync(cartId, cancellation);
        
        cart!.Seats.Add(seat!);

        await this.context.SaveChangesAsync(cancellation);
    }

    public async Task<Cart?> FirstAsync(Guid cartId, CancellationToken cancellation)
    {
        return await this.context.Carts.SingleOrDefaultAsync(e => e.Guid == cartId, cancellation);
    }

    public async Task<IEnumerable<Cart>?> ListAsync(CancellationToken cancellation)
    {
        return await this.context.Carts.ToListAsync(cancellation);
    }
}
