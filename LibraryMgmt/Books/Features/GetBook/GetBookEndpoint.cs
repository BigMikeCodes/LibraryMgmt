using LibraryMgmt.Books.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.GetBook;

/// <summary>
/// Get book by id
/// </summary>
public static class GetBookEndpoint
{
    public const string Name = "GetBook";
    
    public static IEndpointRouteBuilder MapGetBook(this IEndpointRouteBuilder routes)
    {
        routes
            .MapGet("/api/books/{bookId:int}", GetBook)
            .WithName(Name)
            .WithTags("Books")
            .WithDescription("Get a book by id from the library.");
        return routes;
    }

    private static Ok<BookResource> GetBook(
        int bookId,
        [FromServices] Library library)
    {
        var found = library.GetBook(bookId);
        var resource = found.ToResource();
        
        return TypedResults.Ok(resource);
    }
    
}