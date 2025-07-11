using LibraryMgmt.Books.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.RemoveBook;

public static class RemoveBookEndpoint
{
    public static IEndpointRouteBuilder MapDeleteBookEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapDelete("/api/books/{bookId:int}", DeleteBook);
        return routes;
    }
    
    private static NoContent DeleteBook(
        int bookId,
        [FromServices] Library library)
    {
        library.RemoveBook(bookId);
        return TypedResults.NoContent();
    }
}