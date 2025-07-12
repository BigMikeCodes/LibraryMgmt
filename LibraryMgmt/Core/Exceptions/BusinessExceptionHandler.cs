using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Core.Exceptions;

/// <summary>
/// Handler that is responsible for translating exceptions from the domain into ProblemDetails
/// </summary>
public class BusinessExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is AbstractBusinessException businessException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = businessException.Title,
                Detail = businessException.ClientMessage,
                Status = businessException.HttpStatusCode,
                Instance = httpContext.Request.Path
            };
            
            httpContext.Response.StatusCode =businessException.HttpStatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true;
        }

        return false;
    }
}