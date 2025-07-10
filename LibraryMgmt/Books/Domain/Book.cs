namespace LibraryMgmt.Books.Domain;

public class Book
{
    public int Id { get; init; }
    public int AuthorId { get; init; }
    public int PublishedYear { get; init; }
    public string Title { get; init; }
}