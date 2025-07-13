using System.Net;
using LibraryMgmt.Books.Features.AddBook;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration.RemoveBook;

[TestFixture]
public class RemoveBookTests
{
    private WebApplicationFactory<Program> _factory;
    
    [SetUp]
    public void Setup()
    {
        _factory= new WebApplicationFactory<Program>();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
    }

    [Test]
    public async Task Remove_Non_Existent_Book_Returns_404()
    {
        using var client = _factory.CreateClient();
        using var deleteResponse = await client.DeleteBook(100);
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task Remove_Existing_Book_Returns_200()
    {
        using var client = _factory.CreateClient();
        
        var createBook = new AddBookRequest
        {
            Title = "Meditations",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2014
        };
        
        var createBookResponse = await client.CreateBook(createBook);
        var bookId = createBookResponse.BookIdFromLocation() ?? throw new Exception("Book id should not be null");
        
        using var deleteResponse = await client.DeleteBook(bookId);
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
    
}