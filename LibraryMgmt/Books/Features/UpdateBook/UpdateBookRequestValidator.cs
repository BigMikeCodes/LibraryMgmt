using FluentValidation;

namespace LibraryMgmt.Books.Features.UpdateBook;

public class UpdateBookRequestValidator: AbstractValidator<UpdateBookRequest>
{
    public UpdateBookRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AuthorId).NotNull();
        RuleFor(x => x.PublishedYear).NotNull();
        // TODO Regex for isbn number?
        RuleFor(x => x.Isbn).NotEmpty();
    }
}