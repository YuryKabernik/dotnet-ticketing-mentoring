namespace Ticketing.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken);
}