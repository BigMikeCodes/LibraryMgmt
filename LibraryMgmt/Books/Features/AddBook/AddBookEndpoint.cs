using LibraryMgmt.Books.Domain;
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
        // TODO change to a http 201
        var command = Map(request);
        var bookId = library.AddBook(command);
        
        return Results.Ok(bookId);   
    }
}