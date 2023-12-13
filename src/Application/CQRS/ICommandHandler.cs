namespace Ticketing.Application;

public interface ICommandHandler<TRequest>
{
    Task ExecuteAsync(TRequest request, CancellationToken cancellation);
}
