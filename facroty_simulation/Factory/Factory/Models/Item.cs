namespace Factory.Models;

public class Item
{
    public ItemType Type { get; }
    public int Id { get; }
    public int CreatedAtTcik{ get; }

    public Item(ItemType type, int id, int createdAtTcik)
    {
        Type = type;
        Id = id;
        CreatedAtTcik = createdAtTcik;
    }

    public override string ToString()
    {
        return $"Item:{Id},of type {Type}: created at tick{CreatedAtTcik}";
    }
}