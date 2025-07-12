using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryMgmt.Books.Features.GetBooks;

public static class GetBooksEndpoints
{

    public static IEndpointRouteBuilder MapGetBooksEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/books", GetBooks);
        return routes;
    }

    private static Ok<string> GetBooks(
        IValidator<GetBooksParameters> validator,
        [AsParameters] GetBooksParameters pagedRequest)
    {
        return TypedResults.Ok("ok");
    }
}