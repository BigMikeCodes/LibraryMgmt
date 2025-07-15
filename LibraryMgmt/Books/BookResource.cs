namespace LibraryMgmt.Books;

/// <summary>
/// RESTful representation of a book
/// </summary>
public class BookResource
{
    public int Id { get; init; }
    public int AuthorId { get; init; }
    public int PublishedYear { get; init; }
    public required string Isbn { get; init; }
    public required string Title { get; init; }
    
}