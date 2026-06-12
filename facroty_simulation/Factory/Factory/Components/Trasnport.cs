using Factory.Models;

namespace Factory.Components;

public class Transport : ITickable
{
    private readonly Storage _storage;
    private readonly Stock _stock;
    
    private readonly int _arrivalInterval;
    private readonly int _capacityPerArrival;

    public Transport(Storage storage, Stock stock, int arrivalInterval, int capacityPerArrival)
    {
        _storage = storage;
        _stock = stock;
        _arrivalInterval = arrivalInterval;
        _capacityPerArrival = capacityPerArrival;
    }
    public  void Tick(int currentTick)
    {
        if (currentTick % _arrivalInterval != 0)
        {
            return;
        }

        Console.WriteLine("Transport has arrived");
        int loaded = 0;
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            while (loaded < _capacityPerArrival )
            {
                if (_storage.TryTake(type, out Item item))
                {
                    _stock.Add(item);
                    loaded++;
                }
                else
                {
                    break;
                }
            }
            
        }

    }
    
}