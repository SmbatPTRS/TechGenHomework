namespace PageinatedResult.PagedQuery;

public sealed class QueryExecutor<T,TKey> where TKey : IComparable
{
    public static IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> filterRule)
    {
        foreach (var item in items)
        {
            if (filterRule is  null || filterRule(item))
            {
                yield return item;
            }
        }
    }


    public static IReadOnlyList<T> Materiallized<T>(IEnumerable<T> items)
    {
        var array = new List<T>();
        foreach (var item in items)
        {
            array.Add(item);
        }
        return array;
    }

    public static IReadOnlyList<T> Sorter<T, TKey>(IReadOnlyList<T> items, Func<T, TKey> keyReturner, bool descending)
        where TKey : IComparable
    {
        T[] arr = new T[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            arr[i] = items[i];
        }

        Array.Sort(arr, (a, b) =>
        {
            TKey keyA = keyReturner(a); // extract the comparison value from item a (e.g., salary)
            TKey keyB = keyReturner(b); // extract the comparison value from item b

            int comparison = keyA.CompareTo(keyB);

            return descending ? -comparison : comparison;
        });
    return arr;
    }


    public static PagedResult<T> Paginate<T>(IReadOnlyList<T> items, int page, int pageSize)
    {
        page = Math.Max(1, page);
        pageSize = Math.Max(1, pageSize);

        int skip = (page - 1) * pageSize;

        int totalCount = items.Count;

        var pageItems = new List<T>(Math.Min(pageSize, Math.Max(0, totalCount - skip)));


        for (int i = skip; i < items.Count && pageItems.Count < pageSize; i++)
        {
            pageItems.Add(items[i]);
        }

        return new PagedResult<T>(pageItems, page, pageSize,totalCount);
    }


    public static PagedResult<T> Execute<T>(IEnumerable<T> source, QueryOptions<T> options)
    {
        IReadOnlyList<T> filtered = Materiallized(Filter(source, options.filterRule));
        
        return Paginate(filtered, options.page, options.pageSize);
        
    }

    public static PagedResult<T> Execute<T, TKey>(IEnumerable<T> source, QueryOptions<T, TKey> options)
        where TKey : IComparable
    {
        IReadOnlyList<T> filtered = Materiallized(Filter(source, options.filterRule));
//    public static IReadOnlyList<T> Sorter<T, TKey>(IReadOnlyList<T> items, Func<T, TKey> keyReturner, bool descending)

        var res = Sorter(filtered, options.keyReturner, options.descending);
        return Paginate(res, options.page, options.pageSize);
    }
}