using LibraryMgmt.Books.Domain;
using LibraryMgmt.Books.Features.GetBook;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.AddBook;

public static class AddBookEndpoint
{
    public static IEndpointRouteBuilder MapCreateBookEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/books", CreateBook);
        return routes;
    }

    private static Domain.AddBook Map(AddBookRequest request) => 
        new(request.Title,
            request.AuthorId,
            request.PublishedYear,
            request.Isbn);
    
    private static IResult CreateBook(
        [FromBody] AddBookRequest request,
        [FromServices] Library library)
    {
        var command = Map(request);
        var book = library.AddBook(command);
        return TypedResults.CreatedAtRoute(GetBookEndpoint.Name, new { bookId = book.Id });   
    }
}