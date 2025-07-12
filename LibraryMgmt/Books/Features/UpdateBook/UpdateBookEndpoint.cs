using LibraryMgmt.Books.Domain;
using LibraryMgmt.Core.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.Books.Features.UpdateBook;

public static class UpdateBookEndpoint
{
    public static IEndpointRouteBuilder MapUpdateBookEndpoint(this IEndpointRouteBuilder routes)
    {
        routes
            .MapPut("/api/books/{bookId:int}", UpdateBook)
            .WithFluentValidation<UpdateBookRequest>()
            .WithTags("Books")
            .WithDescription("Update an existing book within the library.");
        return routes;
    }

    private static Domain.UpdateBook Map(UpdateBookRequest request, int bookId)
    {
        return new Domain.UpdateBook(
            bookId,
            request.Title,
            request.AuthorId,
            request.PublishedYear,
            request.Isbn);
    }
    
    private static NoContent UpdateBook(
        int bookId,
        [FromBody] UpdateBookRequest request,
        [FromServices] Library library)
    {
        var command = Map(request, bookId);
        library.UpdateBook(command);
        return TypedResults.NoContent();
    }
}