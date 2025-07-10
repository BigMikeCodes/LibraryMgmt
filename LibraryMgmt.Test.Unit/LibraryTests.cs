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

    [Test]
    public void RemoveBook_Removes_Book_From_Library()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        var library = new Library(timeProvider, new Sequence());

        var addBook = new AddBook("REST in Practice", 10, 2025, "978-0596805821");
        var added = library.AddBook(addBook);
        
        var removed = library.RemoveBook(added.Id);
        Assert.That(removed.Id, Is.EqualTo(added.Id));
        
        // Should be able to add the same book without issue after its been removed
        Assert.DoesNotThrow(() => library.AddBook(addBook));
    }
    
    [Test]
    public void RemoveBook_Throws_BookNotFoundException_When_Book_Doesnt_Exist()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        
        // library is empty
        var library = new Library(timeProvider, new Sequence());

        Assert.Throws<BookNotFoundException>(() => library.RemoveBook(100));
    }

    [Test]
    public void GetBook_Returns_CorrectBook()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        
        var library = new Library(timeProvider, new Sequence());
        var addBook = new AddBook("REST in Practice", 10, 2025, "978-0596805821");
        
        var added = library.AddBook(addBook);
        var got = library.GetBook(added.Id);
        
        Assert.That(got, Is.EqualTo(added));
    }    
    
    [Test]
    public void GetBook_Throws_BookNotFoundException_When_Book_Doesnt_Exist()
    {
        var dateSeed = DateTimeOffset.Parse("2025-01-01T00:00:00Z");
        var timeProvider = new FakeTimeProvider(dateSeed);
        
        var sequence = new Sequence();
        var library = new Library(timeProvider, sequence);
        
        var addBook = new AddBook("REST in Practice", 10, 2025, "978-0596805821");
        library.AddBook(addBook);

        Assert.Throws<BookNotFoundException>(() =>
        {
            library.GetBook(sequence.Next());
        });
    }
}