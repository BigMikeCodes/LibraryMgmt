namespace LibraryMgmt.Core.Paging;

public class PagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool SortAscending { get; set; } = true;
}