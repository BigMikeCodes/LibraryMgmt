using System.Net;
using LibraryMgmt.Books;
using LibraryMgmt.Books.Features.AddBook;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration;

public class GetBookTests
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
    public async Task GetBook_That_DoesntExist_ReturnsNotFound_404()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetBook(1);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async Task GetBook_That_Exists_ReturnsBook_200()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "Meditations",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2014
        };

        var createResponse = await client.CreateBook(createBook);
        var location = createResponse.Headers.Location;

        var bookId = int.Parse(location!.ToString().Split("/").Last());

        var bookResponse = await client.GetBook(bookId);
        var resource = await bookResponse.Content.ReadFromJsonAsync<BookResource>();
        
        Assert.Multiple(() =>
        {
            Assert.That(bookResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(resource, Is.Not.Null);
            
            Assert.That(resource!.Title, Is.EqualTo(createBook.Title));
            Assert.That(resource!.AuthorId, Is.EqualTo(createBook.AuthorId));
            Assert.That(resource!.Isbn, Is.EqualTo(createBook.Isbn));
            Assert.That(resource!.PublishedYear, Is.EqualTo(createBook.PublishedYear));
        });
        
    }

}