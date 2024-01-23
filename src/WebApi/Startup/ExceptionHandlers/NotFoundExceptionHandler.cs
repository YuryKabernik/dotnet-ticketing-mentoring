using Microsoft.AspNetCore.Diagnostics;
using Ticketing.Domain.Exceptions;

namespace Ticketing.WebApi.Startup.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
        => exception switch
        {
            NotFoundException => await Write404NotFoundAsync(httpContext, cancellationToken),
            ConflictOnChangeException => await Write409ConflictAsync(httpContext, cancellationToken),
            _ => false
        };

    private static async Task<bool> Write404NotFoundAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        string? targetResource = GetRequestPath(httpContext);
        string errorMessage = targetResource is not null ?
            $"Can't find the requested resource ({targetResource})." :
            $"Can't find the requested resource.";

        await httpContext.Response.WriteAsync(errorMessage, cancellationToken);

        return true;
    }

    private static async Task<bool> Write409ConflictAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        string? targetResource = GetRequestPath(httpContext);
        string errorMessage = targetResource is not null ?
            $"Can't modify the target resource ({targetResource})." :
            $"Can't modify the target requested resource.";

        await httpContext.Response.WriteAsync(errorMessage, cancellationToken);

        return true;
    }

    private static string? GetRequestPath(HttpContext httpContext)
    {
        var handlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
        var targetResource = handlerPathFeature?.Path;
        return targetResource;
    }
}
