using Polly;
using Polly.Extensions.Http;

namespace Ticketing.Notification.Service.HttpPolicies;

internal class RetryHttpPolicy
{
    /// <summary>
    /// For the task it is enough to implement a 3-time retry policy with fixed time intervals.
    /// </summary>
    public static readonly IAsyncPolicy<HttpResponseMessage> Policy =
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(retryCount: 3, retryAttempt => TimeSpan.FromSeconds(1.5));
}
