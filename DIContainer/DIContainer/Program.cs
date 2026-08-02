using System.Data.Common;
using System.Reflection;

namespace DIContainer;

class Program
{
    static void Main(string[] args)
    {
        DIContainer container = new DIContainer();
        
        
        
        container.Register(typeof(ILogger),typeof(ConsoleLogger),LifeTime.Transient);
        
         //registrating via RegisterFactory
         container.RegisterFactory(typeof(IDbConnectionFactory), delegate(DIContainer e)
         {
             //this is where we use DiContainer
             ILogger logger = (ILogger)e.Resolve(typeof(ILogger));
             
             string connectionString = "server==x, connection ==1";
             return new SqlConnectionFactory(connectionString, logger);
         },LifeTime.Singleton);
        
        
        object connectionFactory = container.Resolve(typeof(IDbConnectionFactory));

        Console.WriteLine(connectionFactory);
        
        container.Register<ILogger,ConsoleLogger>(LifeTime.Transient);


        ILogger logger = container.Resolve<ILogger>();
        Type type = logger.GetType();
        MethodInfo[] infos =  type.GetMethods();
        foreach (MethodInfo info in infos)
        {
            Console.WriteLine(info.Name);
        }
        
    }
}