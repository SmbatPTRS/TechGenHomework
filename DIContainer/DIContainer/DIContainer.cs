namespace DIContainer;
using System.Reflection;
public sealed class DIContainer
{
    //Actual container of registries, if someone asks for type x , build type y
    //Type is a runtime C# class, it represents metadata about class, interface or struct
    Dictionary<Type,Type> _registrations = new  Dictionary<Type,Type>();
    
    
}