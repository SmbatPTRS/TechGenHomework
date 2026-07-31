using CsvToolkit;

namespace CsvToolKit;

public sealed class ProductRow
{
    [CsvColumn("Sku", Order = 1)]
    public string Sku { get; set; } = "";

    [CsvColumn("Product Name", Order = 2)]
    public string Name { get; set; } = "";

    [CsvColumn("Unit Price", Order = 3)]
    public decimal Price { get; set; }

    [CsvColumn("In Stock", Order = 4)]
    public bool InStock { get; set; }

    public string? WarehouseCode { get; set; }
    
}