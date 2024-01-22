using Ticketing.Application.Caching;
using Ticketing.Application.CQRS.Caching;
using Ticketing.Application.Feature.Event.Response;

namespace Ticketing.Application.Feature.Event.Requests;

public record EventSeatsBySectionRequest(int EventId, int SectionId) : ICachedQuery<EventSeatsResponse>
{
    public string CacheKey => CacheKeyNames.GetEventSeatsCacheKeyFrom(this.EventId, this.SectionId);

    public TimeSpan? AbsoluteExpiration => default;

    public TimeSpan? SlidingExpiration => default;
}
