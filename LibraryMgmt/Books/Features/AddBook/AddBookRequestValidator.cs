using FluentValidation;

namespace LibraryMgmt.Books.Features.AddBook;

public class AddBookRequestValidator: AbstractValidator<AddBookRequest>
{
    public AddBookRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AuthorId).NotNull();
        RuleFor(x => x.PublishedYear).NotNull();
        // TODO Regex for isbn number?
        RuleFor(x => x.Isbn).NotEmpty();
    }
}