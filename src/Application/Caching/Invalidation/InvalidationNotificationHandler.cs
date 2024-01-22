using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;
using Ticketing.Application.Feature.Carting.UpdateCartSeats.Notifications;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Caching.Invalidation
{
    internal class InvalidationNotificationHandler :
        INotificationHandler<SeatSelectedNotification>,
        INotificationHandler<SeatsBookedNotification>
    {
        private readonly IMemoryCache _cache;

        public InvalidationNotificationHandler(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public Task Handle(SeatSelectedNotification notification, CancellationToken cancellationToken)
        {
            this.InvalidateEventCache(notification.Seat);

            return Task.CompletedTask;
        }

        public Task Handle(SeatsBookedNotification notification, CancellationToken cancellationToken)
        {
            foreach (var seat in notification.Order.Seats)
            {
                this.InvalidateEventCache(seat);
            }

            return Task.CompletedTask;
        }

        private void InvalidateEventCache(EventSeat seat)
        {
            string cacheKey = CacheKeyNames.GetEventSeatsCacheKeyFrom(seat);

            this._cache.Remove(cacheKey);
            this._cache.Remove(CacheKeyNames.Events);
        }
    }
}
