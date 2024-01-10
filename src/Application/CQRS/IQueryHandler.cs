using MediatR;

namespace Ticketing.Application.CQRS;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Application implementation of the query handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellation);
}
