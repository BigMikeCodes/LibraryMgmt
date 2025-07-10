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
    
    public Library()
    {
        _database = new ConcurrentDictionary<int, Book>();
    }

    public Book AddBook(AddBook addBook)
    {
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