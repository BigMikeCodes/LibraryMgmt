using System.Collections.Concurrent;
using LibraryMgmt.Core;

namespace LibraryMgmt.Books.Domain;

/// <summary>
/// Model of a library of books
/// </summary>
public class Library
{
    
    // TODO implement get, add, remove
    // TODO Exceptions
    private readonly ConcurrentDictionary<int, Book> _database;
    private readonly TimeProvider _timeProvider;
    private readonly Sequence _sequence;
    
    public Library(TimeProvider timeProvider, Sequence sequence)
    {
        _timeProvider = timeProvider;
        _database = new ConcurrentDictionary<int, Book>();
        _sequence = sequence;
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

    private void AssertIsbn(string isbn)
    {
        var isbnExists = _database.Values.Any(book => book.Isbn == isbn);
        if (isbnExists)
        {
            throw new IsbnConflictException(isbn);
        }
    }
    
    
    public Book AddBook(AddBook addBook)
    {
        AssertYear(addBook.PublishedYear);
        AssertIsbn(addBook.Isbn);

        var book = new Book
        {
            Id = _sequence.Next(),
            Title = addBook.Title,
            AuthorId = addBook.AuthorId,
            PublishedYear = addBook.PublishedYear,
            Isbn = addBook.Isbn
        };

        _database[book.Id] = book;
        return book;
    }

    public Book RemoveBook(int bookId)
    {
        if (_database.TryRemove(bookId, out var book))
        {
            return book;
        }
        
        throw new BookNotFoundException(bookId);
    }

    public Book GetBook(int bookId)
    {
        if (_database.TryGetValue(bookId, out var book))
        {
            return book;
        }
        // Is this really exceptional? maybe return null & let the caller handle?
        throw new BookNotFoundException(bookId);
    }

    public void UpdateBook(UpdateBook updateBook)
    {
        var book = GetBook(updateBook.BookId);
        AssertYear(updateBook.PublishedYear);

        var otherBooksWithIsbn = _database.Values.Where(b => b.Isbn == updateBook.Isbn && b.Id != book.Id);
        if (otherBooksWithIsbn.Any())
        {
            throw new IsbnConflictException(updateBook.Isbn);
        }
        
        book.Title = updateBook.Title;
        book.Isbn = updateBook.Isbn;
        book.PublishedYear = updateBook.PublishedYear;
        book.AuthorId = updateBook.AuthorId;
        
    }
    
    public IEnumerable<Book> GetBooks()
    {
        return [];
    }
}