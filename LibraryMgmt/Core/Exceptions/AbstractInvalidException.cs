namespace LibraryMgmt.Core.Exceptions;

public abstract class AbstractInvalidException: AbstractBusinessException
{
    public override int HttpStatusCode  => StatusCodes.Status400BadRequest;

    public override string TypeUri => "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request";

    protected AbstractInvalidException(string message) : base(message)
    {
    }
}