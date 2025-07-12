using LibraryMgmt.Core.Exceptions;

namespace LibraryMgmt.Books.Domain;

public class IsbnConflictException: AbstractConflictException
{
    public IsbnConflictException(string isbn) : base($"ISBN: {isbn} already exists within this library")
    {
    }

    public override string Title => "ISBN conflict";
    public override string ClientMessage => Message;
}