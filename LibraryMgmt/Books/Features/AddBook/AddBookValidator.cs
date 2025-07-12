using FluentValidation;

namespace LibraryMgmt.Books.Features.AddBook;

public class AddBookValidator: AbstractValidator<AddBookRequest>
{
    public AddBookValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AuthorId).NotEmpty();
        RuleFor(x => x.PublishedYear).NotEmpty();
        
        // TODO Regex for isbn number?
        RuleFor(x => x.Isbn).NotEmpty();
    }
}