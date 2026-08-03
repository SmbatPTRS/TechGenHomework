namespace DIContainer;

public class NullLogger : ILogger
{
    public void Log(string message)
    {}
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine("[LOG] " + message);
    }
}


public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    
    //--- Several Constructors here to fix the resolving logic gap -----//
    
    // Constructor 1
    public SqlConnectionFactory(string connectionString, ILogger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
        _logger.Log("SqlConnectionFactory created with " + connectionString);
    }
    
    // Constructor 2
    [InjectionConstructor]
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
        _logger = new NullLogger(); // does nothing
    }
    
    // Constructor 3
    public SqlConnectionFactory()
    {
        _connectionString = "default-connection";
        _logger = new NullLogger();
    }
    
}

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger _logger;

    public OrderRepository(IDbConnectionFactory connectionFactory, ILogger logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _logger.Log("OrderRepository created");
    }
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;

    public OrderService(IOrderRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.Log("OrderService created");
    }
}

public class OrderController
{
    private readonly IOrderService _orderService;
    private readonly ILogger _logger;

    public OrderController(IOrderService orderService, ILogger logger)
    {
        _orderService = orderService;
        _logger = logger;
        _logger.Log("OrderController created");
    }
}