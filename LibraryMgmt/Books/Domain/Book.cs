namespace LibraryMgmt.Books.Domain;

public class Book
{
    public required int Id { get; init; }
    public required int AuthorId { get; set; }
    public required int PublishedYear { get; set; }
    public required string Isbn { get; set; }
    public required string Title { get; set; }
}