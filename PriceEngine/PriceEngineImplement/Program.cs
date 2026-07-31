namespace ConsoleApp1;
using System;
using ACA.PriceEngineWrapper;
using ACA.PriceEngine;
class Program
{
    static void Main(string[] args)
    {

        var input = new PriceInput
        {
            Lines = new List<BasketLine>
            {
                new BasketLine { UnitPrice = 25.00M, Quantity = 6 },  
                new BasketLine { UnitPrice = 10.00M, Quantity = 5 } 
            },
            LoyaltyTier = 1,       
            CouponAmount = 15.00M,  
            VatRate = 0.20M        
        };
        var original = new PriceEngine();
        decimal result = original.CalculatePayable(input);

        var wraped = new OrderCorrecter();
        object? fixedresult = wraped.Run(input);
        
        Console.WriteLine($"Original Engine Calculation : {result:C2}");
        Console.WriteLine();
        Console.WriteLine($"Fixed Wrapper Calculation   : {fixedresult:C2}");
    }
}   