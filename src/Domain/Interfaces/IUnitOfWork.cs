using System.Data;

namespace Ticketing.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel readCommitted, CancellationToken cancellationTokens);

    Task SaveChanges(CancellationToken cancellationToken);
}