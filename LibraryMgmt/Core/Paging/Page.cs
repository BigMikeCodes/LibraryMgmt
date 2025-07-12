namespace LibraryMgmt.Core.Paging;

public class Page<T>
{
    public List<T> Data { get; init; } = [];
    public int CurrentPage { get; init; }
    public int TotalPages
    {
        get
        {
            var numPages = TotalItems / PageSize;

            if (TotalItems % PageSize != 0)
            {
                numPages++;
            }
            return numPages;
        }
    }

    public int TotalItems { get; init; }
    public int PageSize { get; init; }
    public bool HasMorePages => CurrentPage < TotalPages;
}