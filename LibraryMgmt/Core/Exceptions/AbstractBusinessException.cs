namespace LibraryMgmt.Core.Exceptions;

/// <summary>
/// Base class for all application "business" exceptions to inherit from.
///
/// Pragmatic choice to use status codes here despite HTTP being a technical concern. An exception in the domain will
/// more often than not result in the client needing to know about it via status code & appropriate error. Better to have
/// this defined in a handful of exception than lots of if/ else all over the place!
/// </summary>
public abstract class AbstractBusinessException: Exception
{
    protected AbstractBusinessException()
    {
    }

    protected AbstractBusinessException(string message): base(message)
    {
    }

    /// <summary>
    /// Title to use for generated ProblemDetails
    /// </summary>
    public abstract string Title { get; }
    
    /// <summary>
    /// Message to use for generated ProblemDetails
    /// </summary>
    public abstract string ClientMessage { get; }
    
    /// <summary>
    /// Status code to return when this is thrown
    /// </summary>
    public abstract int HttpStatusCode { get; }
    
    /// <summary>
    /// URI to information on the type of problem
    /// </summary>
    public abstract string TypeUri { get; }
}