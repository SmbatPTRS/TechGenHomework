using Factory.Components;
using Factory.Config;
using Factory.Models;

namespace Factory;

class Program
{
    static void Main(string[] args)
    {

        SimulationConfig config = SimulationConfig.WithDefaults();
        Random random = new Random(config.RandomSeed);
        
        OrderLine orderLine = new OrderLine(config.OrderLineCapacity);
        Storage storage = new Storage(config.StorageCapacityPerType);
        Stock stock = new Stock(config.StockCapacityPerType);
        QualityChecker qualityChecker = new QualityChecker(orderLine,storage,config.QualityPassPercentage,config.MinQualityCheckTicks,config.MaxQualityCheckTicks,random);
        Transport transport = new Transport(storage, stock, config.TransportArrivalInterval,config.TransportCapacityPerArrival);

        Machine.SetStartId(100);
        Machine machineA = new Machine(1, ItemType.A, config.MachineAInterval, config.MachineATotalItems, orderLine);
        Machine machineB = new Machine(2,ItemType.B, config.MachineBInterval, config.MachineBTotalItems, orderLine);
        Machine machineC = new Machine(3, ItemType.C, config.MachineCInterval, config.MachineCTotalItems, orderLine);
        
        List<ITickable> components = new List<ITickable>
        {
            machineA,
            machineB,
            machineC,
            qualityChecker,
            transport
        };

        for (int tick = 1; tick <= config.TotalTicks; tick++)
        {
            Console.WriteLine($"\n--- Tick {tick} ---");
            foreach (ITickable component in components)
            {
                component.Tick(tick);
            }
        }

        Console.WriteLine("\n=== Simulation Complete===");
        Console.WriteLine(stock.ToString());

    }
}