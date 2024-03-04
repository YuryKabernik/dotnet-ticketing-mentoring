using HealthChecks.UI.Client;

namespace Ticketing.WebApi.Startup.HealthChecks;

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

    public static HealthChecksUIBuilder UseHealthCheckUIConfigured(this IServiceCollection services) =>
        services.AddHealthChecksUI(setup =>
         {
             setup.SetApiMaxActiveRequests(3);
             setup.SetEvaluationTimeInSeconds(5);
             setup.MaximumHistoryEntriesPerEndpoint(50);

             setup.AddHealthCheckEndpoint("liveness", LivenessEndpoint);
             setup.AddHealthCheckEndpoint("readiness", ReadinessEndpoint);
         })
        .AddInMemoryStorage();

}
