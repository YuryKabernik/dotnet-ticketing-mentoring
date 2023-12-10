namespace Ticketing.Domain.Interfaces;

public interface IRepository<TEntity, TEntityId>
{
    Task<TEntity?> FirstAsync(TEntityId entityId, CancellationToken cancellation);
    Task<IEnumerable<TEntity>?> ListAsync(CancellationToken cancellation);
}
