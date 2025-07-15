using FluentValidation;
using LibraryMgmt.Books.Domain;
using LibraryMgmt.Core.Filters;
using LibraryMgmt.Core.Paging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.GetBooks;

public static class GetBooksEndpoints
{

    public static IEndpointRouteBuilder MapGetBooksEndpoint(this IEndpointRouteBuilder routes)
    {
        routes
            .MapGet("/api/books", GetBooks)
            .WithFluentValidation<GetBooksParameters>()
            .WithDescription("Returns a collection of books from the library.")
            .WithTags("Books");
        
        return routes;
    }

    private static Ok<Page<BookResource>> GetBooks(
        IValidator<GetBooksParameters> validator,
        [AsParameters] GetBooksParameters request,
        [FromServices] Library library)
    {
        //TODO convert this to a RFC ProblemDetails, potentially use a filter?
        validator.ValidateAndThrow(request);

        var books = library.GetBooks();
        books.Sort(new BookComparer(request.SortAscendingOrDefault));
        
        var page = books.PageAndMap(
            request.PageSizeOrDefault,
            request.PageNumberOrDefault,
            book => book.ToResource());
        
        return TypedResults.Ok(page);
    }
}