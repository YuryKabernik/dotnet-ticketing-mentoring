namespace Ticketing.Application.CQRS;

public interface IQueryHandler<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request);
}
