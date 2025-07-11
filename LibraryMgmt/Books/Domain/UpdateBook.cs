namespace LibraryMgmt.Books.Domain;

public record UpdateBook(
    int BookId,
    string Title,
    int AuthorId,
    int PublishedYear,
    string Isbn);