namespace LibraryMgmt.Core.Exceptions;

public abstract class AbstractConflictException: AbstractBusinessException
{
    
    public override int HttpStatusCode =>  StatusCodes.Status409Conflict;

    public override string  TypeUri =>  "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.10";
    
    protected AbstractConflictException(string message): base(message)
    {
    }
    
}