namespace LibraryMgmt.Core.Paging;

public class Page<T>
{
    public List<T> Data { get; init; } = [];
    public required int CurrentPage { get; init; }
    public required int TotalItems { get; init; }
    public required int PageSize { get; init; }
    
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
    
    public bool HasMorePages => CurrentPage < TotalPages;
}