namespace ExercicesGenerics.ex2;

public class Filter
{
    public static List<T> filter<T>(List<T> source, Predicate<T> predicate)
    {
        List<T> res = new List<T>();
        foreach (var i in source)
        {
            bool? suffice = predicate?.Invoke(i) ?? false;
            if (suffice.Value)
            {
                res.Add(i);
            }
        }
        return res;
    }

    public static bool IsEaven(int item)
    {
        return (item % 2) == 0;
    }

    public static List<TOut> Project<TIn, TOut>(List<TIn> input, Func<TIn, TOut> transformation)
    {
        var result = new List<TOut>();
        foreach (var i in input)
        {
            result.Add(transformation(i));
        }
        return result;
    }

    public static string Transform(int item)
    {
        return $"N{item}";
    }
    
}