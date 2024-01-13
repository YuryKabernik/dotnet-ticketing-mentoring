using MediatR;

namespace Ticketing.Application.CQRS;

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
}

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
}
