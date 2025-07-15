using LibraryMgmt.Books.Domain;
using LibraryMgmt.Books.Features.GetBook;
using LibraryMgmt.Core.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.AddBook;

public static class AddBookEndpoint
{
    public static IEndpointRouteBuilder MapCreateBookEndpoint(this IEndpointRouteBuilder routes)
    {
        routes
            .MapPost("/api/books", CreateBook)
            .WithFluentValidation<AddBookRequest>()
            .WithTags("Books")
            .WithDescription("Add a book to the library.");
        return routes;
    }

    private static Domain.AddBook Map(AddBookRequest request) => 
        new(request.Title!,
            request.AuthorId!.Value,
            request.PublishedYear!.Value,
            request.Isbn!);
    
    private static CreatedAtRoute CreateBook(
        [FromBody] AddBookRequest request,
        [FromServices] Library library)
    {
        var command = Map(request);
        var book = library.AddBook(command);
        return TypedResults.CreatedAtRoute(GetBookEndpoint.Name, new { bookId = book.Id });   
    }
}