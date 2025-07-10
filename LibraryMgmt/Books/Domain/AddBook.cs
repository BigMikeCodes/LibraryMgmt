namespace LibraryMgmt.Books.Domain;

public record AddBook(string Title, int AuthorId, int PublishedYear, string Isbn);