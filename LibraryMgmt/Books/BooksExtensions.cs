using LibraryMgmt.Books.Domain;
using LibraryMgmt.Books.Features.AddBook;

namespace LibraryMgmt.Books;

public static class BooksExtensions
{
    public static IEndpointRouteBuilder AddBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapCreateBookEndpoint();
        return routes;
    }

    public static IServiceCollection AddBooksServices(this IServiceCollection services)
    {
        services.AddSingleton<Library>(_ => new Library());
        return services;
    }
    
}