namespace PageinatedResult.PagedQuery;

public class QueryOptions<T>
{
    public Func<T,bool>? filterRule { get;init; }
    public int pageSize { get; init; } = 3;
    public int page { get; init; } = 1;
    public bool descending { get;init; }
}