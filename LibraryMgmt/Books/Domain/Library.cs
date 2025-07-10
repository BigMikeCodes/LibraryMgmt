using System.Collections.Concurrent;

namespace LibraryMgmt.Books.Domain;

/// <summary>
/// Model of a library of books
/// </summary>
public class Library
{
    
    // TODO implement get, add, remove
    // TODO atomic counter for book id
    // TODO Exceptions
    private readonly ConcurrentDictionary<int, Book> _database;
    private TimeProvider _timeProvider;
    
    public Library(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        _database = new ConcurrentDictionary<int, Book>();
    }

    private int GetCurrentYear() => _timeProvider.GetUtcNow().Year;

    private void AssertYear(int year)
    {
        var currentYear = GetCurrentYear();
        if (year > currentYear)
        {
            throw new InvalidYearException(year, currentYear);
        }
    }
    
    
    public Book AddBook(AddBook addBook)
    {
        AssertYear(addBook.PublishedYear);
        
        
        throw new NotImplementedException();
    }

    public Book RemoveBook(int bookId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetBooks()
    {
        return [];
    }
}