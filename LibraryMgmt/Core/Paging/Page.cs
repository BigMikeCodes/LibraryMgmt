namespace LibraryMgmt.Core.Paging;

public class Page<T>
{
    public IReadOnlyList<T> Data { get; init; } = [];
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; init; }
    public bool HasMorePages => CurrentPage < TotalPages;
}