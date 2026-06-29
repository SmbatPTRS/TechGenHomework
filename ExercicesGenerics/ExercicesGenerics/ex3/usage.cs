namespace ExercicesGenerics.ex3;

public class usage
{
    public static T Creator<T>() where T : Iinitializeable, new()
    {
        T res = new T();
        res.initialize();
        
        if (res.IsInitialized)
        {
            return res;
        }
        throw new  InvalidOperationException("The initializer is not initialized");
    }
}


public class DatabaseConnection : Iinitializeable
{
    public bool IsInitialized { get; private set; }

    public void initialize()
    {
        IsInitialized = true;
    }
}
