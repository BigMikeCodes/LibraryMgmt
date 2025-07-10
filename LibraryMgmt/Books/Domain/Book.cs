namespace LibraryMgmt.Books.Domain;

public class Book
{
    public required int Id { get; init; }
    public required int AuthorId { get; init; }
    public required int PublishedYear { get; init; }
    public required string Isbn { get; init; }
    public required string Title { get; init; }
}