using LibraryMgmt.Core.Exceptions;

namespace LibraryMgmt.Books.Domain;

public class BookNotFoundException: AbstractNotFoundException
{
    public BookNotFoundException(int bookId): base($"Book with id: {bookId} not found")
    {
    }
    
    public override string Title => "Book not found";
    public override string ClientMessage => Message;
}