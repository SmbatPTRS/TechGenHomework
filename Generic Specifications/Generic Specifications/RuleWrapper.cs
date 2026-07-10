namespace Generic_Specifications;

public class RuleWrapper<T> : ISpecification<T>
{
    private readonly Func<T, bool> _predicate;
    
    public RuleWrapper(Func<T, bool> predicate)
    {
        _predicate = predicate;
    }

    public bool IsSatisfiedBy(T entity)
    {
        return _predicate(entity);
    }
}