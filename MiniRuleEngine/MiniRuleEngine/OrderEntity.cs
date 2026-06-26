namespace MiniRuleEngine;

public class OrderEntity : IEntity
{
    public int Id { get; set; }
    public string EntityType => "Order";
    public string CustomerName { get; set; }
    public decimal Amount { get; set; }
    public int ItemCount { get; set; }
    
    public OrderEntity( decimal amount, string customerName, int itemCount)
    {
        Amount = amount;
        CustomerName = customerName;
        ItemCount = itemCount;
    }
}