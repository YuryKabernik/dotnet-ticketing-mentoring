using MediatR;

namespace Ticketing.Application;

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    Task ExecuteAsync(TRequest request, CancellationToken cancellation);
}
