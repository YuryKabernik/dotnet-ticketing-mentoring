using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Interfaces;

namespace Ticketing.DataAccess.Repositories;

public class CartRepository : IRepository<Cart, Guid>
{
    private readonly DataContext context;

    public CartRepository(DataContext context)
    {
        this.context = context;
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
