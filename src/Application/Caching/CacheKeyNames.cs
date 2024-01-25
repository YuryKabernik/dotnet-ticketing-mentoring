using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Caching
{
    internal class CacheKeyNames
    {
        /// <summary>
        /// Template key name formatted by event id (0) and section id {1}.
        /// </summary>
        public const string EventSeatsByEventSection = "event-{0}-section-{1}-seats";

        /// <summary>
        /// Factory method to create EventSeats cache key from an event seat.
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public static string GetEventSeatsCacheKeyFrom(EventSeat seat) =>
            string.Format(EventSeatsByEventSection, seat.Row.Section.Event.Id, seat.Row.Section.Id);

        /// <summary>
        /// Factory method to create EventSeats cache key from event id and section id.
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public static string GetEventSeatsCacheKeyFrom(int eventId, int sectionId) =>
            string.Format(EventSeatsByEventSection, eventId, sectionId);

        /// <summary>
        /// Key name for events data.
        /// </summary>
        public const string Events = "events";
    }
}
