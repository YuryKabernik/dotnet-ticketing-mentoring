using Microsoft.AspNetCore.Mvc;

namespace Ticketing.WebApi.Caching
{
    public static class CacheProfileRegister
    {
        /// <summary>
        /// 
        /// </summary>
        public const string EventsProfileName = "CacheEvents";

        /// <summary>
        /// Extension method to control response cache to ‘Event’ resource endpoints (/events/*) using request (HTTP) caching,
        /// allow client applications perform cache management on their side and avoid making additional requests to the server with no need.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MvcOptions AddCacheProfileEvents(this MvcOptions options)
        {
            CacheProfile profile = new()
            {
                Duration = TimeSpan.FromSeconds(15).Seconds,
                Location = ResponseCacheLocation.Client,
                NoStore = false,
            };

            options.CacheProfiles.Add(EventsProfileName, profile);

            return options;
        }
    }
}
