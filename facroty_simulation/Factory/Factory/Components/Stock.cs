using System.Text;
using Factory.Models;

namespace Factory.Components;

public class Stock
{
    private readonly Dictionary<ItemType, List<Item>> _stocks;
    private readonly int _capacityPerType;
    
    public Stock(int capacityPerType)
    {
        _stocks = new Dictionary<ItemType, List<Item>>();
        _capacityPerType = capacityPerType;
        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            _stocks.Add(itemType,new List<Item>());
        }
        
    }

    public void Add(Item item)
    {
        if (_stocks[item.Type].Count >= _capacityPerType)
        {
            Console.WriteLine("Stock is  full");
            return;
        }
        _stocks[item.Type].Add(item);
        Console.WriteLine($"Added {item.Type} to stock");
    }

    public int CountOf(ItemType itemType)
    {
        return _stocks[itemType].Count;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Stock");
        foreach (KeyValuePair<ItemType, List<Item>> pair in _stocks)
        {
            ItemType type = pair.Key;
            List<Item> list = pair.Value;
            
            int count  = list.Count;
            sb.AppendLine($"- Type: {type} | Total Items: {count}");
        }
        return sb.ToString();
    }
}