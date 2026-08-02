namespace DIContainer;

public class Registration
{
    public LifeTime LifeTime { get; set; }
    public Type ImplementationType { get; set; }
    
    // if we need to reuse something, Singleton case
    public object? Instance { get; set; }
}