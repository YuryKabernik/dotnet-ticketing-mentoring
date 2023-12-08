namespace Ticketing.Domain;

public interface IRepository<TEntity>
{
    Task<TEntity?> FirstAsync(int id, CancellationToken cancellation);
    Task<IEnumerable<TEntity>?> ListAsync(CancellationToken cancellation);
}
