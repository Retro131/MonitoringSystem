using InfotecsBackend.Errors.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsBackend.Errors.Handlers;
#pragma warning disable CS1591

public class NotFoundExceptionsHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        httpContext.Response.StatusCode = (int)notFoundException.HttpStatusCode;
        problemDetails.Title = notFoundException.Message;
        problemDetails.Status = httpContext.Response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}