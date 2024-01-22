using Ticketing.Application.Caching;
using Ticketing.Application.CQRS.Caching;
using Ticketing.Application.Feature.Event.Response;

namespace Ticketing.Application.Feature.Event.Requests;

public record EventsRequest() : ICachedQuery<EventsResponse>
{
    public string CacheKey => CacheKeyNames.Events;

    public TimeSpan? AbsoluteExpiration => default;

    public TimeSpan? SlidingExpiration => default;
}
