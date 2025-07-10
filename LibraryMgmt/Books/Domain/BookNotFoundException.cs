namespace LibraryMgmt.Books.Domain;

public class BookNotFoundException: ArgumentException
{
    public BookNotFoundException(int bookId): base($"Book with id: {bookId} not found")
    {
    }
}