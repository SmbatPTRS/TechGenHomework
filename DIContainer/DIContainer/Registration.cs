namespace DIContainer;

public class Registration
{
    public LifeTime LifeTime { get; private set; }
    public Type ImplementationType { get; private set; }
    
    // if we need to reuse something, Singleton case
    public object? Instance { get; set; }
    
    public Func<DIContainer,object> Factory { get; private set; }

    private Registration(LifeTime lifetime)
    {
        LifeTime = lifetime;
    }


    public static Registration ForType(Type implementationType, LifeTime lifetime)
    {
        Registration registration = new Registration(lifetime);
        registration.ImplementationType = implementationType;
        return registration;
    }

    public static Registration ForFactory(Func<DIContainer, object> factory, LifeTime lifetime)
    {
        Registration registration = new Registration(lifetime);
        registration.Factory = factory;
        return registration;
    }
}