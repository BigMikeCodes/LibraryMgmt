using FluentValidation;

namespace LibraryMgmt.Books.Features.GetBooks;

public class GetsBooksParametersValidator: AbstractValidator<GetBooksParameters>
{
    public GetsBooksParametersValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1);
        
    }
}