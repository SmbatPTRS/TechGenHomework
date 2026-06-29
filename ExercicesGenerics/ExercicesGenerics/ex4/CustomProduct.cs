namespace ExercicesGenerics.ex4;

public class CustomProduct : IComparable<CustomProduct>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public CustomProduct(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public int CompareTo(CustomProduct? other)
    {
        if (other == null)
        {
            return 1;
        }

        return Price.CompareTo(other.Price);
    }

    public static List<CustomProduct> SortNSelect(List<CustomProduct> list, int quantity)
    {
        if (quantity > list.Count)
        {
            return list;
        }
        list.Sort();
        list.Reverse();
        return list.GetRange(0, quantity);
    }
}