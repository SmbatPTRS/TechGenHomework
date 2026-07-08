namespace PageinatedResult.PagedQuery;

public sealed class QueryOptions<T, TKey> : QueryOptions<T> where TKey : IComparable
{
    public Func<T,TKey> keyReturner { get; init; }
}