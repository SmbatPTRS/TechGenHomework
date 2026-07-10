namespace Generic_Specifications;

// Why is it that here I don't need to write <T> after the interface name?
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}