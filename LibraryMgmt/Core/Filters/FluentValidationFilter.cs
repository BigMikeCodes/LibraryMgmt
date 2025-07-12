using FluentValidation;

namespace LibraryMgmt.Core.Filters;

/// <summary>
/// Endpoint filter to perform validation on an incoming request
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public class FluentValidationFilter<TRequest>: IEndpointFilter
{
    
    private readonly IValidator<TRequest> _validator;
    private readonly ILogger<FluentValidationFilter<TRequest>> _logger;

    public FluentValidationFilter(
        IValidator<TRequest> validator,
        ILogger<FluentValidationFilter<TRequest>> logger)
    {
        _validator = validator;
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var toValidate = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (toValidate is null)
        {
            _logger.LogError("Expected argument of type {} not found for path {}", nameof(TRequest), context.HttpContext.Request.Path);
            return await next(context);
        }
        
        var validationResult = await _validator.ValidateAsync(toValidate);
        if (validationResult.IsValid)
        {
            return await next(context);
        }
        
        _logger.LogInformation("Validation failed for path {}", context.HttpContext.Request.Path);
        return TypedResults.ValidationProblem(validationResult.ToDictionary());
        
    }
}