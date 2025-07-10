using LibraryMgmt.Books.Domain;
using Microsoft.Extensions.Time.Testing;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class LibraryTests
{
    
    [Test]
    public void AddBook_Throws_InvalidArgumentException_When_Year_Is_In_Future()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        var library = new Library(timeProvider);
        
        var addBook = new AddBook("REST in Practice", 10, 2026, "978-0596805821");
        
        Assert.Throws<InvalidYearException>(() =>
        {
            library.AddBook(addBook);
        });
    }
    
    
}