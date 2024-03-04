using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ticketing.Notification.Startup;

public static class HealthCheckEndpoints
{
    private static readonly string LivenessEndpoint = "/health/live";
    private static readonly string ReadinessEndpoint = "/health/ready";

    public static IEndpointConventionBuilder MapLivenessEndpoint(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks(LivenessEndpoint, options: new()
        {
            Predicate = (check) => check.Tags.Contains("live"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

    public static IEndpointConventionBuilder MapReadinessEndpoint(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks(ReadinessEndpoint, options: new()
        {
            Predicate = (check) => check.Tags.Contains("ready"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
}
