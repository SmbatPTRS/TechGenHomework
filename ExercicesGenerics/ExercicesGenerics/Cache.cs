namespace ExercicesGenerics;

public sealed class CacheWrapper<T>
{
    public T data { get; set; }
    public DateTime date;

    public CacheWrapper(T data)
    {
        this.data = data;
        this.date = DateTime.Now;
    }
}

public class Cache<T>
{
    private int TTL;
    private Dictionary<string, CacheWrapper<T>> cache;
    public Cache(int ttl)
    {
        this.cache = new Dictionary<string, CacheWrapper<T>>();
        this.TTL = ttl;
    }

    public void Add(string key, T data)
    {
        CacheWrapper<T> wrapper = new CacheWrapper<T>(data);
        this.cache.Add(key, wrapper);
    }
    public bool ContainsKey(string key)
    {
        bool  result = this.cache.TryGetValue(key, out CacheWrapper<T> ?wrapper);
            
        return result;
    }

    public T? tryGet(string? key)
    {
        if (ContainsKey(key))
        {
            CacheWrapper<T> wrapper = cache[key];
            if ((DateTime.Now - wrapper.date).TotalSeconds > TTL){
                cache.Remove(key);
                return default;
            }
            return wrapper.data;
        }
        return default;
    }
}



