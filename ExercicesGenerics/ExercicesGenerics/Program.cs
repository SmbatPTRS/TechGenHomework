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
        // var result = Filter<int>.filter(myList,Filter<int>.isEaven);
        // var res1 = Filter<int>.project(result, Filter<int>.transform);
        // foreach (var item in res1)
        // {
        //     Console.WriteLine(item);
        // }
        // =================================================================================


        //============= 3rd Exercice =======================================================
        // var connection = usage.Creator<DatabaseConnection>();
        // Console.WriteLine(connection.IsInitialized);
        //================================================================================
        
        
        
        // ============= 4th Exercice =====================================================
        // Product p1 = new Product("CocaCola", 2);
        // Product p2 = new Product("Pepsi", 3);
        // Product p3 = new Product("NoteBookLm", 4);
        // Product p4 = new Product("Football", 5);
        // Product p5 = new Product("Parachutespecial", 6);
        //
        // Comparer1 comparer = new Comparer1();
        // TopNBuffer<Product> buffer = new TopNBuffer<Product>(3, comparer);
        // buffer.Add(p1);
        // buffer.Add(p2);
        // buffer.Add(p3);
        // buffer.Add(p4);
        // buffer.Add(p5);
        //
        // Console.WriteLine("Top 3 products by longest name:");
        // foreach (var p in buffer.Snapshot())
        // {
        //     Console.WriteLine($"  {p} — name length: {p.Name.Length}");
        // }     
        
        //===================================================================================




        // CustomProduct product = new("cola", 10);
        //
        // Func<CustomProduct> op = () =>
        // {
        //     if (product.Price < 20)
        //     {
        //         product.Price += 10;
        //         throw new Exception("price too low, increased to " + product.Price);
        //     }
        //     return product;
        // };
        //
        // Func<Exception, bool> retry = (ex) => true;
        //
        //
        //
        // Result<CustomProduct> result =  Result<CustomProduct>.Execute(op, 3, retry);
        // Console.WriteLine(result.Value);
        // Console.WriteLine(result.Attempts);
        // Console.WriteLine(result.ErrorMessage);
        // Console.WriteLine(result.IsSuccess);
        //
        //
        //
        // CustomProduct x = new  CustomProduct("x", 10);
        // CustomProduct y = new  CustomProduct("y", 10);
        //
        // x.CompareTo(y);
    }



    
    


}