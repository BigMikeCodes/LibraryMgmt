using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Core.Exceptions;

/// <summary>
/// Final catch all for exceptions that no other handler can handle.
/// </summary>
public class UnhandledExceptionHandler: IExceptionHandler
{
    
    private readonly ILogger<UnhandledExceptionHandler> _logger;

    public UnhandledExceptionHandler(ILogger<UnhandledExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occured!");

        var problemDetails = new ProblemDetails
        {
            Title = "An error occured",
            Detail = "An error occured when processing the request",
            Instance = httpContext.Request.Path,
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#section-15.6.1",
            Status = StatusCodes.Status500InternalServerError
        };
        
        // Remember be vague, don't disclose stack traces to the client!
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}