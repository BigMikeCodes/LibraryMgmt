using Microsoft.AspNetCore.Mvc.Testing;

namespace LibraryMgmt.Test.Integration;

public class Tests
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
    public async Task Test1()
    {
        var client = _factory.CreateClient();
        var result = await client.GetAsync("/api/books/1");
        
        Assert.Pass();
    }
}