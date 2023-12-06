namespace Ticketing.Domain;

public interface IRepository<TEntity>
{
    Task<TEntity?> FirstAsync(int id);
    Task<IEnumerable<TEntity>?> ListAsync();
}
