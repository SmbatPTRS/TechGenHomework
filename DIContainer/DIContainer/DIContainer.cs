namespace DIContainer;
using System.Reflection;
public sealed class DIContainer
{
    //Actual container of registries, if someone asks for type x , build type y
    //Type is a runtime C# class, it represents metadata about class, interface or struct
    Dictionary<Type,Registration> _registrations = new  Dictionary<Type,Registration>();


    //To not allow illegal registrations such as ->
    //container.Register(typeof(IDBConnection), typeof(OrderRepository)
    public void Register<TService, TImplementation>(LifeTime lifeTime) where TImplementation : TService
    {
        Register(typeof(TService), typeof(TImplementation),lifeTime);
        
    }

    public void Register(Type serviceType, Type implementationType, LifeTime lifetime)
    {
        Registration registration = Registration.ForType(implementationType,lifetime);
        
        _registrations[serviceType] = registration;
    }
    
    
    public void RegisterFactory(Type serviceType, Func<DIContainer, object> factory, LifeTime lifetime)
    {
        Registration registration = Registration.ForFactory(factory, lifetime);
        _registrations[serviceType] = registration;
    }


    public T Resolve<T>()
    {
        object result = Resolve(typeof(T));
        
        return (T)result;
    }

    public object? Resolve(Type serviceType)
    {
        //getting the corresponding Registration for a given service type
        Registration registration = _registrations[serviceType];

        
        if (registration.LifeTime == LifeTime.Singleton && registration.Instance != null)
        {
            return registration.Instance;
        }

        object instance;

        if (registration.Factory != null)
        {
            // Hand the container itself ("this") to the custom function,
            // so it can resolve its own sub-dependencies if it needs to.
            instance = registration.Factory(this);
        }
        else
        {
            //taking the marked Consturctor or the one with most parameters
            ConstructorInfo constructorInfo = SelectConstructor(registration.ImplementationType);

        
            //taking the parameters from the ctor
            ParameterInfo[] parameterInfos = constructorInfo.GetParameters();

            object[] arguments = new object[parameterInfos.Length];

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                arguments[i] = Resolve(parameterInfos[i].ParameterType);
            }
            instance = constructorInfo.Invoke(arguments);
            
        }
        //if singleton, keep it for later usage     
        if (registration.LifeTime == LifeTime.Singleton)
        {
            registration.Instance = instance;
        }
        return instance;
    }



    // Logic to select the constructor, marked by attribute or with most parameters
    private ConstructorInfo SelectConstructor(Type implementationType)
    {
        ConstructorInfo[] constructors = implementationType.GetConstructors();

        if (constructors.Length == 0)
        {
            throw new InvalidOperationException($"{implementationType.Name} doesnt have public constructor");
        }
        
        
        ConstructorInfo? marked =  null;

        for (int i = 0; i < constructors.Length; i++)
        {
            ConstructorInfo current = constructors[i];

            InjectionConstructorAttribute attribute = current.GetCustomAttribute<InjectionConstructorAttribute>();

            if (attribute != null)
            {
                marked = current;
                break;
            }
        }

        if (marked != null)
        {
            return marked;
        }

        ConstructorInfo bestSoFar = constructors[0];

        int bestParameterNumber = bestSoFar.GetParameters().Length;

        for (int i = 1; i < constructors.Length; i++)
        {
            ConstructorInfo current  = constructors[i];
            int currentParameterCount = current.GetParameters().Length;

            if (currentParameterCount > bestParameterNumber)
            {
                bestSoFar = current;
                bestParameterNumber = currentParameterCount;
            }
            
        }
        return bestSoFar;
        
    }
}