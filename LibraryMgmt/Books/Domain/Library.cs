using System.Collections.Concurrent;
using LibraryMgmt.Core.Sequences;

namespace LibraryMgmt.Books.Domain;

/// <summary>
/// Model of a library of books
/// </summary>
public class Library
{
    
    private readonly ConcurrentDictionary<int, Book> _database;
    private readonly TimeProvider _timeProvider;
    private readonly ISequence<int> _sequence;
    
    public Library(TimeProvider timeProvider, ISequence<int> sequence)
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

    /// <summary>
    /// Determine if the library contains any books with the provided ISBN but not with the provided id
    /// </summary>
    /// <param name="isbn">ISBN to check</param>
    /// <param name="bookId">BookId to exclude</param>
    /// <returns></returns>
    private bool HasOtherBookWithIsbn(string isbn, int bookId)
    {
        return _database.Values.Any(b => b.Isbn == isbn && b.Id != bookId);
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

        if (HasOtherBookWithIsbn(updateBook.Isbn, updateBook.BookId))
        {
            throw new IsbnConflictException(updateBook.Isbn);
        }
        
        book.Title = updateBook.Title;
        book.Isbn = updateBook.Isbn;
        book.PublishedYear = updateBook.PublishedYear;
        book.AuthorId = updateBook.AuthorId;
        
    }
    
    public List<Book> GetBooks()
    {
        return _database.Values.ToList();
    }
}