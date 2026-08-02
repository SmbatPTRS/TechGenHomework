namespace DIContainer;

public interface ILogger
{
    void Log(string message);
}

public interface IDbConnectionFactory { }

public interface IOrderRepository { }

public interface IOrderService { }
