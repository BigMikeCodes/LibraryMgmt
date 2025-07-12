using System.ComponentModel.DataAnnotations;

namespace LibraryMgmt.Books.Features.AddBook;

public class AddBookRequest
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int PublishedYear { get; set; }
    public string Isbn { get; set; }
}