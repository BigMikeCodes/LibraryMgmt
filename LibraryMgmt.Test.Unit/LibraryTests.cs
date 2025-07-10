using LibraryMgmt.Books.Domain;
using LibraryMgmt.Core;
using Microsoft.Extensions.Time.Testing;

namespace LibraryMgmt.Test.Unit;

[TestFixture]
public class LibraryTests
{
    
    [Test]
    public void AddBook_Throws_InvalidYearException_When_Year_Is_In_Future()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        var library = new Library(timeProvider, new Sequence());
        
        var addBook = new AddBook("REST in Practice", 10, 2026, "978-0596805821");
        
        Assert.Throws<InvalidYearException>(() =>
        {
            library.AddBook(addBook);
        });
    }
    
    [Test]
    public void AddBook_Throws_IsbnConflictException_When_Isbn_Is_In_Library()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        var library = new Library(timeProvider, new Sequence());

        var addBook = new AddBook("REST in Practice", 10, 2025, "978-0596805821");
        library.AddBook(addBook);
        
        Assert.Throws<IsbnConflictException>(() =>
        {
            library.AddBook(addBook);
        });
    }
    
}