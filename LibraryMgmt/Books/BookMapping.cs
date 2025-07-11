using LibraryMgmt.Books.Domain;

namespace LibraryMgmt.Books;

public static class BookMapping
{
    public static BookResource ToResource(this Book book)
    {
        return new BookResource
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }
}