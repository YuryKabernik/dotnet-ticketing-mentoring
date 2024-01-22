using MediatR;

namespace Ticketing.Application.CQRS.Caching;

internal interface ICachedQuery<TResponse> : ICachedQuery, IRequest<TResponse>
{
}

internal interface ICachedQuery
{
    public string CacheKey { get; }

    public TimeSpan? AbsoluteExpiration { get; }

    public TimeSpan? SlidingExpiration { get; }
}
