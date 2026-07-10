namespace Generic_Specifications;

class Program
{
    static void Main(string[] args)
    {
        ExampleEntityBook camus     = new ExampleEntityBook { Title = "Lhomme Revolte",     IsAvailable = true,  Price = 12, Genre = "Literature" };
        ExampleEntityBook orwell   = new ExampleEntityBook { Title = "1984",     IsAvailable = false, Price = 8,  Genre = "Dystopian" };
        ExampleEntityBook nietzche   = new ExampleEntityBook { Title = "zaratustra",   IsAvailable = true,  Price = 25, Genre = "Mystery" };
        ExampleEntityBook marcus = new ExampleEntityBook { Title = "contemplations", IsAvailable = true,  Price = 15, Genre = "philosophy" };

        
        var isAvailable = RuleCreatorExtension.Create<ExampleEntityBook>(b=>b.IsAvailable);
        var isCheap =  RuleCreatorExtension.Create<ExampleEntityBook>(b=>b.Price<20);
        var isMystery = RuleCreatorExtension.Create<ExampleEntityBook>(b=>b.Genre == "Mystery");


        var cheapOrMystery      = isCheap.Or(isMystery);

        // why is this not working with try catch but works in regular case
        // try
        // {
        //     var isAvailableAndCheap = RuleCreatorExtension.AllOf(isAvailable, isCheap);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        // }
        var isAvailableAndCheap = RuleCreatorExtension.AllOf(isAvailable, isCheap);
        Console.WriteLine(camus.Title + " " + isAvailable.IsSatisfiedBy(camus));
        Console.WriteLine(orwell.Title + " " + isAvailableAndCheap.IsSatisfiedBy(orwell));
        
            
    }
}