using System.ComponentModel.DataAnnotations;

namespace LibraryMgmt.Books.Features.AddBook;

public class AddBookRequest
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int PublishedYear { get; set; }
    
    [Required]
    public string Isbn { get; set; }
}