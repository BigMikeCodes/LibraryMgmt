namespace LibraryMgmt.Core.Paging;

public class PagedRequest
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public bool? SortAscending { get; set; }
    
    public int PageNumberOrDefault => PageNumber ?? 1;
    public int PageSizeOrDefault => PageSize ?? 10;
    public bool SortAscendingOrDefault => SortAscending ?? true;
}