namespace LibraryMgmt.Books.Features.UpdateBook;

public class UpdateBookRequest
{
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
    public int? PublishedYear { get; set; }
    public string? Isbn { get; set; }
}