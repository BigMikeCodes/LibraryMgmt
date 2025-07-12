namespace LibraryMgmt.Core.Exceptions;

public abstract class AbstractNotFoundException: AbstractBusinessException
{
    public override int HttpStatusCode => StatusCodes.Status404NotFound;

    public override string TypeUri => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.5";

    protected AbstractNotFoundException(string message) : base(message)
    {
    }
}