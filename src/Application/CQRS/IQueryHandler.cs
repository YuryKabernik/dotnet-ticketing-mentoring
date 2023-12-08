namespace Ticketing.Application.CQRS;

public interface IQueryHandler<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellation);
}
