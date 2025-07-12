using LibraryMgmt.Books.Domain;
using LibraryMgmt.Books.Features.AddBook;
using LibraryMgmt.Books.Features.GetBook;
using LibraryMgmt.Books.Features.GetBooks;
using LibraryMgmt.Books.Features.RemoveBook;
using LibraryMgmt.Books.Features.UpdateBook;
using LibraryMgmt.Core.Sequences;

namespace LibraryMgmt.Books;

public static class BooksExtensions
{
    public static IEndpointRouteBuilder AddBooksEndpoints(this IEndpointRouteBuilder routes)
    {
        return routes
            .MapCreateBookEndpoint()
            .MapGetBookEndpoint()
            .MapDeleteBookEndpoint()
            .MapGetBooksEndpoint()
            .MapUpdateBookEndpoint();
    }

    public static IServiceCollection AddBooksServices(this IServiceCollection services)
    {
        services.AddSingleton<Library>(sp =>
        {
            var sequence = new AtomicIntSequence();
            var timeProvider = sp.GetService<TimeProvider>()!;
            
            return new Library(timeProvider, sequence);
        });
        return services;
    }
    
}