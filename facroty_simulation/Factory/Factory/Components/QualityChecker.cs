using Factory.Models;

namespace Factory.Components;

public class QualityChecker : ITickable
{
    private readonly OrderLine _orderLine;
    private readonly Storage _storage;
    private readonly Random _random;
    private readonly int _passPercentage;
    private readonly int _minTicks;
    private readonly int _maxTicks;
    
    private Item _currentItem;
    private int _finishAtTick;


    public QualityChecker(OrderLine orderLine, Storage storage, int passPercentage, int minTicks, int maxTicks, Random random)
    {
        _orderLine = orderLine;
        _storage = storage;
        _passPercentage = passPercentage;
        _minTicks = minTicks;
        _maxTicks = maxTicks;
        _random = random;
    }


    public void Tick(int currentTick)
    {
        if (_currentItem != null)
        {
            if (currentTick < _finishAtTick){return;}

            if (currentTick >= _finishAtTick)
            {
                int roll =  _random.Next(0,99);
                if (roll < _passPercentage)
                {
                    Console.WriteLine($"Item {_currentItem.Id} PASSED");
                    _storage.Add(_currentItem);    
                }
                else
                {
                    Console.WriteLine($"Item {_currentItem.Id} FAILED, dropped");
                }

                _currentItem = null;
            }
        }

        if (_currentItem == null)
        {
            if (_orderLine.TryDequeue(out Item item) == false)
            {
                return;
            }
            _currentItem = item;
            int randomDiration = _random.Next(_minTicks,_maxTicks+1);
            _finishAtTick = currentTick + randomDiration;
            return;
        }
    }
}