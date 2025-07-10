namespace LibraryMgmt.Books.Domain;

public class IsbnConflictException: ArgumentException
{
    public IsbnConflictException(string isbn) : base($"ISBN: {isbn} already exists within this library")
    {
    }
}