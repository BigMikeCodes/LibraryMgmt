using LibraryMgmt.Books.Domain;
using LibraryMgmt.Books.Features.AddBook;
using LibraryMgmt.Books.Features.GetBook;
using LibraryMgmt.Books.Features.RemoveBook;
using LibraryMgmt.Core;

namespace LibraryMgmt.Books;

public static class BooksExtensions
{
    public static IEndpointRouteBuilder AddBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        return routes
            .MapCreateBookEndpoint()
            .MapGetBook()
            .MapDeleteBookEndpoint();
    }

    public static IServiceCollection AddBooksServices(this IServiceCollection services)
    {
        services.AddSingleton<Library>(sp =>
        {
            var sequence = new Sequence();
            var timeProvider = sp.GetService<TimeProvider>()!;
            
            return new Library(timeProvider, sequence);
        });
        return services;
    }
    
}