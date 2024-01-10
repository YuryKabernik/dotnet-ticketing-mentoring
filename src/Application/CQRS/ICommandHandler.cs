using MediatR;

namespace Ticketing.Application.CQRS;

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    /// <summary>
    /// Application implementation of the command handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task ExecuteAsync(TRequest request, CancellationToken cancellation);
}
