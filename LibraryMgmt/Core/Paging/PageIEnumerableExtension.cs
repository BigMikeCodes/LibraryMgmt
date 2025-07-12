namespace LibraryMgmt.Core.Paging;

public static class PageIEnumerableExtension
{
    /// <summary>
    /// Create a page from an existing IEnumerable
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Page<T> Page<T>(this IEnumerable<T> data, int pageSize, int pageNumber)
    {
        var offset = pageSize * (pageNumber - 1);
        var dataList = data.ToList();
        var taken = dataList.Take(new Range(offset, offset + pageSize));

        return new Page<T>
        {
            CurrentPage = pageNumber,
            Data = taken.ToList(),
            PageSize = pageSize,
            TotalItems = dataList.Count
        };
    }
}