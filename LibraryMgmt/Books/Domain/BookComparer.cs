namespace LibraryMgmt.Books.Domain;

public class BookComparer: Comparer<Book>
{

    private readonly int _sortFactor;

    public BookComparer(bool sortAscending)
    {
        // Flip between ASC & DESC
        _sortFactor = sortAscending ? 1 : -1;
    }

    public override int Compare(Book? x, Book? y)
    {
        if (x is null && y is null)
        {
            return 0;
        }

        if (x is null)
        {
            return -1 * _sortFactor;
        }
        
        if (y is null)
        {
            return 1 * _sortFactor;
        }

        // Title
        if (string.Compare(x.Title, y.Title, StringComparison.InvariantCultureIgnoreCase) != 0)
        {
            return string.Compare(x.Title, y.Title, StringComparison.InvariantCultureIgnoreCase) * _sortFactor;
        }

        // PublishedYear
        if (x.PublishedYear.CompareTo(y.PublishedYear) != 0)
        {
            return x.PublishedYear.CompareTo(y.PublishedYear) * _sortFactor;
        }
        
        // Fallback to Id 
        return x.Id.CompareTo(y.Id) * _sortFactor;
        
    }
}