namespace ExercicesGenerics.ex2;

public class Filter<T>
{
    public static IEnumerable<T> filter(IEnumerable<T> source, Predicate<T> predicate)
    {
        foreach (var i in source)
        {
            if (predicate(i))
            {
                yield return i;
            }
        }
    }

    public static bool isEaven(int item)
    {
        return item % 2 == 0;
    }
    
    public static string transform(int item)
    {
        return $"N{item}";
    }

    public static IEnumerable<TOut> project<TIn, TOut>(IEnumerable<TIn> source, Func<TIn, TOut> func)
    {
        foreach (var i in source)
        {
            if (func != null)
            {
                yield return func(i);
            }
        }
    }
}