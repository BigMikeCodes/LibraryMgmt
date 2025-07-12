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
        return data.PageAndMap(pageSize, pageNumber, i => i);
    }


    /// <summary>
    /// Create a page from an existing IEnumerable, mapping the requested page's data as per the provided mapper.
    /// 
    /// This will only map data that ends up in the final page.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="mapper">Func used to map from TSource to TMapTo</param>
    /// <typeparam name="TSource">Type from the source IEnumerable</typeparam>
    /// <typeparam name="TMapTo"></typeparam>
    /// <returns></returns>
    public static Page<TMapTo> PageAndMap<TSource, TMapTo>(
        this IEnumerable<TSource> data,
        int pageSize,
        int pageNumber,
        Func<TSource, TMapTo> mapper)
    {
        var offset = pageSize * (pageNumber - 1);
        var dataList = data.ToList();
        var taken = dataList.Take(new Range(offset, offset + pageSize));

        return new Page<TMapTo>
        {
            CurrentPage = pageNumber,
            Data = taken.Select(mapper).ToList(),
            PageSize = pageSize,
            TotalItems = dataList.Count
        };
    }
}