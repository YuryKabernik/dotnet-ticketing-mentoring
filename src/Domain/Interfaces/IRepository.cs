namespace Ticketing.Domain.Interfaces;

public interface IRepository<TEntity, TEntityId>
{
    Task<TEntity> GetAsync(TEntityId id, CancellationToken cancellation);
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellation);
}
