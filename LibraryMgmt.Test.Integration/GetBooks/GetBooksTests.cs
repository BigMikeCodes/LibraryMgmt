using System.Net;
using LibraryMgmt.Books;
using LibraryMgmt.Books.Features.AddBook;
using LibraryMgmt.Core.Paging;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration.GetBooks;

[TestFixture]
public class GetBooksTests
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
    public async Task Empty_Library_Returns_Empty_Page_200()
    {
        const int pageSize = 10;
        const int pageNumber = 1;
        
        var client  = _factory.CreateClient();
        var response = await client.GetBooks(pageSize, pageNumber, true);
        var page = await response.Content.ReadFromJsonAsync<Page<BookResource>>();
        
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(page!.PageSize, Is.EqualTo(pageSize));
            Assert.That(page.CurrentPage, Is.EqualTo(pageNumber));
            Assert.That(page.HasMorePages, Is.False);
            Assert.That(page.Data, Is.Empty);
        });
    }

    [TestCase(-1, -1)]
    [TestCase(0, 1)]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    public async Task Invalid_Params_Cause_400(int pageSize, int pageNumber)
    {
        var client  = _factory.CreateClient();
        var response = await client.GetBooks(pageSize, pageNumber, true);
        
        Assert.Multiple(() =>
        {
            // TODO can expand this to check the response instead of just status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        });
    }    
    
    [Test]
    public async Task Books_Sorted_As_Expected_200()
    {
        var client  = _factory.CreateClient();

        List<AddBookRequest> booksToAdd = [
            new()
            {
                Title = "AAA",
                AuthorId = 1,
                Isbn = "978-1503280465",
                PublishedYear = 2014
            },
            new()
            {
                Title = "AAA",
                AuthorId = 1,
                Isbn = "978-1503280466",
                PublishedYear = 2015
            },
            new()
            {
                Title = "ZZZ",
                AuthorId = 1,
                Isbn = "978-1503280467",
                PublishedYear = 2000
            },
            new()
            {
                Title = "ZZZ",
                AuthorId = 1,
                Isbn = "978-1503280468",
                PublishedYear = 2005
            }
        ];

        foreach (var request in booksToAdd)
        {
            await client.CreateBook(request);
        }
        
        var responseAsc = await client.GetBooks(10, 1, true);
        var responseDesc = await client.GetBooks(10, 1, false);

        var ascPage = await responseAsc.Content.ReadFromJsonAsync<Page<BookResource>>();
        var descPage = await responseDesc.Content.ReadFromJsonAsync<Page<BookResource>>();
        
        Assert.Multiple(() =>
        {
            Assert.That(responseAsc.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseDesc.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
            Assert.That(ascPage!.Data, Has.Count.EqualTo(booksToAdd.Count));
            Assert.That(descPage!.Data, Has.Count.EqualTo(booksToAdd.Count));
            
            Assert.That(ascPage.Data, 
                Is.Ordered.Ascending.By(nameof(BookResource.Title))
                        .Then.Ascending.By(nameof(BookResource.PublishedYear))
                        .Then.Ascending.By(nameof(BookResource.Id)));
            
            Assert.That(descPage.Data, 
                Is.Ordered.Descending.By(nameof(BookResource.Title))
                        .Then.Descending.By(nameof(BookResource.PublishedYear))
                        .Then.Descending.By(nameof(BookResource.Id)));
            
        });
    }
    
}