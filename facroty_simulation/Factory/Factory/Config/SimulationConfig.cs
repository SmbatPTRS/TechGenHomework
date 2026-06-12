namespace Factory.Config;

public class SimulationConfig
{
    public int TotalTicks { get; set; }
    public int StartItemId { get; set; }
    public int RandomSeed { get; set; }
    
    public int OrderLineCapacity{get;set;}
    
    public int StorageCapacityPerType {get;set;}
    public int StockCapacityPerType {get;set;}
    
    public int MinQualityCheckTicks {get;set;}
    public int MaxQualityCheckTicks {get;set;}
    public int QualityPassPercentage {get;set;}
    
    public int TransportArrivalInterval {get;set;}
    public int TransportCapacityPerArrival {get;set;}
    
    public int MachineAInterval {get;set;}
    public int MachineBInterval {get;set;}
    public int MachineCInterval {get;set;}
    
    
    public int MachineATotalItems {get;set;}
    public int MachineBTotalItems {get;set;}
    public int MachineCTotalItems {get;set;}
    
    public static SimulationConfig WithDefaults()
    {
        return new SimulationConfig
        {
            TotalTicks = 35,
            StartItemId = 100,
            RandomSeed = 42,
            OrderLineCapacity = 5,
            StorageCapacityPerType = 50,
            StockCapacityPerType = 200,
            MinQualityCheckTicks = 1,
            MaxQualityCheckTicks = 3,
            QualityPassPercentage = 80,
            TransportArrivalInterval = 4,
            TransportCapacityPerArrival = 6,
            MachineAInterval = 1,
            MachineBInterval = 2,
            MachineCInterval = 3,
            MachineATotalItems = 35,
            MachineBTotalItems = 18,
            MachineCTotalItems = 12,
        };
    }
}