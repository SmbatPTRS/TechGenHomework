namespace PageinatedResult.PagedQuery;


public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int page { get; } = 1;
    public int pageSize { get; } = 3;
    
    public int totalCount { get; }
    public int totalPages =>  (int)Math.Ceiling((double)totalCount / pageSize);
    
    public PagedResult(IReadOnlyList<T> items, int page, int pageSize,int totalCount)
    {
        Items = items;
        this.page = page;
        this.pageSize = pageSize;
        this.totalCount = totalCount;
    }
    
}