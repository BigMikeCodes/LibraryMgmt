using LibraryMgmt.Core.Exceptions;

namespace LibraryMgmt.Books.Domain;

public class InvalidYearException: AbstractInvalidException
{
    public InvalidYearException(int providedYear, int currentYear)
        : base($"Invalid year {providedYear}, current year is {currentYear}")
    {
    }

    public override string Title => "Invalid year provided";
    public override string ClientMessage => Message;
}