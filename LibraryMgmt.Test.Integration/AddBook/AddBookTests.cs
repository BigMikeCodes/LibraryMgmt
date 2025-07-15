using System.Net;
using LibraryMgmt.Books.Features.AddBook;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration.AddBook;

[TestFixture]
public class AddBookTests
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
    public async Task Book_Added_As_Expected_201()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "Meditations",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2014
        };
        
        var response = await client.CreateBook(createBook);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);
        });
    }

    [Test]
    public async Task Isbn_Conflict_Returns_Conflict_409()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "Meditations",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = 2014
        };
        
        var responseOne = await client.CreateBook(createBook);
        var responseTwo = await client.CreateBook(createBook);
        
        Assert.Multiple(() =>
        {
            Assert.That(responseOne.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseOne.Headers.Location, Is.Not.Null);
            
            Assert.That(responseTwo.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        });
    }
    
    [Test]
    public async Task Invalid_Year_Returns_Bad_Request_400()
    {
        using var client = _factory.CreateClient();
        var createBook = new AddBookRequest
        {
            Title = "Meditations",
            AuthorId = 1,
            Isbn = "978-1503280465",
            PublishedYear = DateTime.Now.AddYears(100).Year,
        };
        
        var response = await client.CreateBook(createBook);
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        });
    }

    [Test]
    public async Task Invalid_Request_Returns_Bad_Request_400()
    {
        var request = new AddBookRequest
        {
            AuthorId = null,
            Isbn = null,
            PublishedYear = null,
            Title = null
        };
        
        using var client = _factory.CreateClient();
        using var response = await client.CreateBook(request);
        
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        var errors = problemDetails!.GetErrors();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            
            // Expect an error for each field in the request
            Assert.That(errors, Contains.Key(nameof(AddBookRequest.AuthorId)));
            Assert.That(errors, Contains.Key(nameof(AddBookRequest.Title)));
            Assert.That(errors, Contains.Key(nameof(AddBookRequest.Isbn)));
            Assert.That(errors, Contains.Key(nameof(AddBookRequest.PublishedYear)));

        });
        

    }
}