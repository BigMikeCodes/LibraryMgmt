using System.Net;
using LibraryMgmt.Books;
using LibraryMgmt.Books.Features.AddBook;
using LibraryMgmt.Books.Features.UpdateBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration.UpdateBook;

[TestFixture]
public class UpdateBookTests
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
    public async Task Update_Non_Existent_Book_Returns_404()
    {
        using var client = _factory.CreateClient();

        var updateBook = new UpdateBookRequest
        {
            Title = "Non Existent Book should return a 404",
            AuthorId = 10,
            PublishedYear = 20,
            Isbn = "978-1503280465"
        };
        
        using var response = await client.UpdateBook(updateBook, 100);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Update_existing_Book_To_Existing_Isbn_Returns_Conflict_409()
    {
        // Create 2 books.
        // Try to update the second book to the isbn of the first
        using var client = _factory.CreateClient();
        var createBookOne = new AddBookRequest
        {
            Title = "ONE",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2000,
        };
        
        await client.CreateBook(createBookOne);
        
        var createBookTwo = new AddBookRequest
        {
            Title = "TWO",
            AuthorId = 2,
            Isbn = "978-1503280222",
            PublishedYear = 2000,
        };
        using var bookTwoResponse = await client.CreateBook(createBookTwo);
        var bookTwoId = bookTwoResponse.BookIdFromLocation() ?? throw new Exception("Book ID not found");
        
        var updateBookTwo = new UpdateBookRequest
        {
            Title = "This is an update",
            AuthorId = 100,
            Isbn = createBookOne.Isbn,
            PublishedYear = 2000
        };
        
        using var updateBookTwoResponse = await client.UpdateBook(updateBookTwo, bookTwoId);
        
        Assert.That(updateBookTwoResponse.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }

    [Test]
    public async Task Update_existing_Book_Invalid_Year_Returns_400()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "ONE",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2000,
        };
        
        using var createResponse = await client.CreateBook(createBook);
        var id = createResponse.BookIdFromLocation() ?? throw new Exception("Book ID not found");

        var updateBook = new UpdateBookRequest
        {
            Title = "Updated",
            AuthorId = 10,
            Isbn = createBook.Isbn,
            PublishedYear = DateTime.Now.Year + 1,
        };

        var updateResponse = await client.UpdateBook(updateBook, id);
        
        Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Update_Book_Updates_Book_As_Expected_200()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "ONE",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2000,
        };
        
        using var createResponse = await client.CreateBook(createBook);
        var id = createResponse.BookIdFromLocation() ?? throw new Exception("Book ID not found");
        
        var updateBook = new UpdateBookRequest
        {
            Title = "Updated",
            AuthorId = 10,
            Isbn = "978-1503280222",
            PublishedYear = 1900,
        };
        
        using var updateBookResponse = await client.UpdateBook(updateBook, id);
        
        using var getBookResponse = await client.GetBook(id);
        var bookResource = await getBookResponse.Content.ReadFromJsonAsync<BookResource>();
        
        Assert.Multiple(() =>
        {
            Assert.That(updateBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            
            Assert.That(bookResource, Is.Not.Null);
            
            Assert.That(bookResource!.Title, Is.EqualTo(updateBook.Title));
            Assert.That(bookResource.AuthorId, Is.EqualTo(updateBook.AuthorId));
            Assert.That(bookResource.Isbn, Is.EqualTo(updateBook.Isbn));
            Assert.That(bookResource.PublishedYear, Is.EqualTo(updateBook.PublishedYear));
        });
        
        
    }

    [Test]
    public async Task Invalid_Request_Returns_Bad_Request_400()
    {
        var request = new UpdateBookRequest
        {
            AuthorId = null,
            Isbn = null,
            PublishedYear = null,
            Title = null
        };
        
        using var client = _factory.CreateClient();
        using var response = await client.UpdateBook(request, 10);
        
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        var errors = problemDetails!.GetErrors();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            
            // Expect an error for each field in the request
            Assert.That(errors, Contains.Key(nameof(UpdateBookRequest.AuthorId)));
            Assert.That(errors, Contains.Key(nameof(UpdateBookRequest.Title)));
            Assert.That(errors, Contains.Key(nameof(UpdateBookRequest.Isbn)));
            Assert.That(errors, Contains.Key(nameof(UpdateBookRequest.PublishedYear)));
        });
    }
    
}