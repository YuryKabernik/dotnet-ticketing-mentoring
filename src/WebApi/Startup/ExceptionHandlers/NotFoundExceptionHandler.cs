using Microsoft.AspNetCore.Diagnostics;
using Ticketing.Domain.Exceptions;

namespace Ticketing.WebApi.Startup.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var handlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            var targetResource = handlerPathFeature?.Path;

            if (exception is NotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;


                string errorMessage = targetResource is not null ?
                    $"Can't find the requested resource ({targetResource})." :
                    $"Can't find the requested resource.";

                await httpContext.Response.WriteAsync(errorMessage, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
