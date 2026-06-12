using Factory.Models;
using Factory.Config;
namespace Factory.Components;

public class Machine : ITickable
{
    private readonly ItemType _itemType;
    private readonly int _interval;
    private readonly int _totalItems;
    private  readonly OrderLine _orderLine;
    private readonly int _machineId;
    private static int _nextItemId=100;
    private int _producedCount;
    
    public static void SetStartId(int startId)
    {
        _nextItemId = startId;
    }
    public Machine(int MachineId, ItemType itemType, int interval, int totalItems, OrderLine orderLine)
    {
        _itemType = itemType;
        _interval = interval;
        _totalItems = totalItems;
        _orderLine = orderLine;
        _machineId = MachineId;
    }

    public void Tick(int currentTick)
    {
        if (_producedCount >= _totalItems)
        {
            return;
        }

        if (currentTick % _interval != 0)
        {
            return;
        }
        int itemId = _nextItemId;
        Item item = new Item(_itemType, _nextItemId, currentTick);
        bool result = _orderLine.TryEnqueue(item);
        if (result)
        {
            _nextItemId++;
            _producedCount++;
            Console.WriteLine($"Machine {_machineId}: produced item {item.Id}");
        }
        else
        {
            Console.WriteLine($"Machine {_machineId}: OrderLine full, item skipped");
        }

    }
}