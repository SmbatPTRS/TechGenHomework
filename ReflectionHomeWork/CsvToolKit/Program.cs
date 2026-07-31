namespace CsvToolKit;

class Program
{
    static void Main(string[] args)
    {
        List<ProductRow> originalProducts = new List<ProductRow>
        {
            new ProductRow
            {
                Sku = "TEA-1",
                Name = "Armenian Tea",
                Price = 4.50m,
                InStock = true,
                WarehouseCode = "WH-A"
            },
            new ProductRow
            {
                Sku = "COF-2",
                Name = "Coffee, Premium",
                Price = 9.99m,
                InStock = false,
                WarehouseCode = "WH-B"
            },

            new ProductRow
            {
                Sku = "MUG-3",
                Name = "The \"Big\" Mug, 500ml",
                Price = 12.50m,
                InStock = true,
                WarehouseCode = "WH-C"
            }
            
            
        };
        
        string csvText = CsvSerializer.WriteAll(originalProducts);
        Console.WriteLine(csvText);
        Console.WriteLine();
        
        List<ProductRow> importedProducts = CsvDeserializer.ReadAll<ProductRow>(csvText);
        Console.WriteLine("=== IMPORTED OBJECTS ===");
        foreach (var item in importedProducts)
        {
            Console.WriteLine($"SKU: {item.Sku} | Name: {item.Name} | Price: {item.Price} | InStock: {item.InStock} | WarehouseCode: {item.WarehouseCode ?? "null"}");
        }
    }

}