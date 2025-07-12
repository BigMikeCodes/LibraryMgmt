namespace LibraryMgmt.Core.Filters;

public static class FluentValidationFilterExtension
{

    /// <summary>
    /// Adds a filter to this builder that will use a FluentValidation IValidator to short circuit if validation fails,
    /// returning a http 400 ProblemDetails
    /// </summary>
    /// <param name="routeBuilder"></param>
    /// <typeparam name="T">Type to validate on the incoming request</typeparam>
    /// <returns></returns>
    public static RouteHandlerBuilder WithFluentValidation<T>(this RouteHandlerBuilder routeBuilder)
    {
        return routeBuilder
            .AddEndpointFilter<FluentValidationFilter<T>>()
            .ProducesValidationProblem();
    }
    
}