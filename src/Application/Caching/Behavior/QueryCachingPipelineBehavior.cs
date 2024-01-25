using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Ticketing.Application.CQRS.Caching;
using Ticketing.Domain.Exceptions;

namespace Ticketing.Application.Caching
{
    internal class QueryCachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery<TResponse>
    {
        private readonly IMemoryCache _cache;

        public QueryCachingPipelineBehavior(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse? result = await this._cache.GetOrCreateAsync(request.CacheKey, cacheEntry =>
            {
                cacheEntry.SetSize(1)
                    .SetAbsoluteExpiration(request.AbsoluteExpiration ?? TimeSpan.FromSeconds(90))
                    .SetSlidingExpiration(request.SlidingExpiration ?? TimeSpan.FromSeconds(15));

                return next();
            });

            return result ?? throw new NotFoundException("Cached response wasn't found!");
        }
    }
}
