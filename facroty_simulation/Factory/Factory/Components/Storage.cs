using System.Text;
using Factory.Models;

namespace Factory.Components;

public class Storage
{
    private readonly Dictionary<ItemType, List<Item>> _items;
    private readonly int _capacityPerType;

    public Storage(int capacityPerType)
    {
        _capacityPerType = capacityPerType;
        _items = new Dictionary<ItemType, List<Item>>();
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            _items.Add(type, new List<Item>());
        }
    }

    public void Add(Item item)
    {
        if (_items[item.Type].Count >= _capacityPerType)
        {
            Console.WriteLine("Too many items in storage");
            return;
        }

        _items[item.Type].Add(item);
        Console.WriteLine("Added item to storage");
    }

    public bool TryTake(ItemType type, out Item item)
    {
        if (_items[type].Count == 0)
        {
            item = null;
            return false;
        }

        item = _items[type][0];
        _items[type].RemoveAt(0);
        return true;
    }

    public int CountOf(ItemType type)
    {
        return _items[type].Count;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Current Inventory Summary:");

        foreach (KeyValuePair<ItemType, List<Item>> pair in _items)
        {
            ItemType type = pair.Key;
            List<Item> list = pair.Value;

            int count = list.Count;

            sb.AppendLine($"- Type: {type} | Total Items: {count}");
        }

        return sb.ToString();
    }
}