using MediatR;

namespace Ticketing.Application.CQRS;

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
}
