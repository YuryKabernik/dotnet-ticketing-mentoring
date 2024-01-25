using Microsoft.AspNetCore.Diagnostics;
using Ticketing.Domain.Exceptions;

namespace Ticketing.WebApi.Startup.ExceptionHandlers;

public class DomainExceptionHandler(ILogger<DomainExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<DomainExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) => exception switch
        {
            NotFoundException => await this.Write404NotFoundAsync(httpContext, exception, cancellationToken),
            ConflictOnChangeException => await this.Write409ConflictAsync(httpContext, exception, cancellationToken),
            _ => false
        };

    private async Task<bool> Write404NotFoundAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        string? targetResource = GetRequestPath(httpContext);
        string errorMessage = targetResource is not null ?
            $"Can't find the requested resource ({targetResource})." :
            $"Can't find the requested resource.";

        await httpContext.Response.WriteAsync(errorMessage, cancellationToken);
        this._logger.LogInformation(eventId: 1, exception, errorMessage);

        return true;
    }

    private async Task<bool> Write409ConflictAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        string? targetResource = GetRequestPath(httpContext);
        string errorMessage = targetResource is not null ?
            $"Can't modify the target resource ({targetResource})." :
            $"Can't modify the target requested resource.";

        await httpContext.Response.WriteAsync(errorMessage, cancellationToken);
        this._logger.LogCritical(eventId: 5, exception, errorMessage);

        return true;
    }

    private static string? GetRequestPath(HttpContext httpContext)
    {
        var handlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();

        return handlerPathFeature?.Path;
    }
}
