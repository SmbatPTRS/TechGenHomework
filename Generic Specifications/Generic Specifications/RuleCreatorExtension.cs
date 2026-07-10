namespace Generic_Specifications;

public static class RuleCreatorExtension
{
    public static ISpecification<T> Create<T>(Func<T, bool> predicate)
    {
        return new RuleWrapper<T>(predicate);
    }
    
    public static ISpecification<T> AllOf<T>(params ISpecification<T>[] specs)
    {
        if (specs.Length == 0)
            throw new ArgumentException("AllOf requires at least one specification.");

        ISpecification<T> result = specs[0];
        for (int i = 1; i < specs.Length; i++)
        {
            result = result.And(specs[i]);
        }
        return result;
    }

    public static ISpecification<T> AnyOf<T>(params ISpecification<T>[] specs)
    {
        if (specs.Length == 0)
            throw new ArgumentException("AnyOf requires at least one specification.");

        ISpecification<T> result = specs[0];
        for (int i = 1; i < specs.Length; i++)
        {
            result = result.Or(specs[i]);
        }
        return result;
    }
    
}