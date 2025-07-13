using LibraryMgmt.Books;
using LibraryMgmt.Books.Features.AddBook;
using LibraryMgmt.Books.Features.UpdateBook;

namespace LibraryMgmt.Test.Integration;

/// <summary>
/// Extension to HttpClient that adds extensions for the book endpoints, should help to keep tests cleaner
/// </summary>
public static class BookHttpClientExtensions
{
    private const string ApiBasePath = "/api/books";
    
    /// <summary>
    /// Get a book by id
    /// </summary>
    /// <param name="client"></param>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> GetBook(this HttpClient client, int bookId)
    {
        return await client.GetAsync($"{ApiBasePath}/{bookId}");
    }

    /// <summary>
    /// Get a paged collection of books
    /// </summary>
    /// <param name="client"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="sortAscending"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> GetBooks(this HttpClient client, int pageSize, int pageNumber, bool sortAscending)
    {
        var uri = $"{ApiBasePath}?PageNumber={pageNumber}&SortAscending={sortAscending}&PageSize={pageSize}";
        return await client.GetAsync(uri);
    }
    
    /// <summary>
    /// Create a new book
    /// </summary>
    /// <param name="client"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> CreateBook(this HttpClient client, AddBookRequest request)
    {
        return await client.PostAsJsonAsync(ApiBasePath, request);
    }

    /// <summary>
    /// Update an existing book
    /// </summary>
    /// <param name="client"></param>
    /// <param name="request"></param>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> UpdateBook(this HttpClient client, UpdateBookRequest request, int bookId)
    {
        return await client.PutAsJsonAsync($"{ApiBasePath}/{bookId}", request);
    }

    /// <summary>
    /// Delete an existing book
    /// </summary>
    /// <param name="client"></param>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> DeleteBook(this HttpClient client, int bookId)
    {
        return await client.DeleteAsync($"{ApiBasePath}/{bookId}");
    }
    
}