using FluentValidation;
using LibraryMgmt.Books.Domain;
using LibraryMgmt.Core.Paging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.GetBooks;

public static class GetBooksEndpoints
{

    public static IEndpointRouteBuilder MapGetBooksEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/books", GetBooks);
        return routes;
    }

    private static Ok<Page<Book>> GetBooks(
        IValidator<GetBooksParameters> validator,
        [AsParameters] GetBooksParameters request,
        [FromServices] Library library)
    {
        //TODO convert this to a RFC ProblemDetails, potentially use a filter?
        validator.ValidateAndThrow(request);

        var books = library.GetBooks();
        var page = books.Page(request.PageSize, request.PageNumber);
        
        return TypedResults.Ok(page);
    }
}