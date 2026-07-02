using ExercicesGenerics.ex2;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ExercicesGenerics.ex3;
using ExercicesGenerics.ex4;
using ExercicesGenerics.ex5;

namespace ExercicesGenerics;

class Program
{


    static void Main(string[] args)
    {
        // ==============  1st Exercice Implementation ===================================
        // GenericPair<string, int> a = new GenericPair<string, int>("A", 1);
        // Console.WriteLine(a.First);
        // Console.WriteLine(a.Second);
        // var x = GenericPair<string, int>.Swap(a);
        // Console.WriteLine(x.First);
        // Console.WriteLine(x.Second);
        // ================================================================================

        
        
        // ============= 2nd Exercice Implementation ======================================
        // List<int> myList = new List<int>() { 1, 2, 3, 4 };
        //
        // var step2 = Filter.Project(step1, Filter.Transform);
        //
        // foreach (var item in step2)
        // {
        //     Console.WriteLine(item);
        // }
        // =================================================================================


        //============= 3rd Exercice =======================================================
        // var connection = usage.Creator<DatabaseConnection>();
        // Console.WriteLine(connection.IsInitialized);
        //================================================================================
        
        
        
        // ============= 4th Exercice =====================================================
        // CustomProduct p1 = new("cola", 15);
        // CustomProduct p2 = new("pepsi", 25);
        // CustomProduct p3 = new("computer", 300);
        // CustomProduct p4 = new("super_computer", 400);
        //
        // List<CustomProduct> myList = new() { p1, p2, p3, p4 };
        //
        //
        // List<CustomProduct>res = CustomProduct.SortNSelect(myList,2);
        // foreach (CustomProduct p in res)
        // {
        //     Console.WriteLine($"{p.Name} - {p.Price}");
        // }
        //===================================================================================
        
        
        
        
        CustomProduct product = new("cola", 10);

        Func<CustomProduct> op = () =>
        {
            if (product.Price < 20)
            {
                product.Price += 10;
                throw new Exception("price too low, increased to " + product.Price);
            }
            return product;
        };

        Func<Exception, bool> retry = (ex) => true;


        Result<CustomProduct> result =  Result<CustomProduct>.Execute(op, 3, retry);
        Console.WriteLine(result.Value);
        Console.WriteLine(result.Attempts);
        Console.WriteLine(result.ErrorMessage);
        Console.WriteLine(result.IsSuccess);
    }



    
    


}