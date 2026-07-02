namespace ExercicesGenerics.ex4;

public sealed class TopNBuffer<T>
{
    private T[] _buffer;
    private int _count; // number of actual elements in the buffer
    
    IComparer<T> _comparer;
    public TopNBuffer(int size, IComparer<T>? comparer)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }
        _buffer = new T[size];
        _count = 0;
        _comparer = comparer ??  Comparer<T>.Default;
    }

    public void Add(T item)
    {
        if (_count < _buffer.Length)
        {
            AddDescending(item);
            _count++;
            return;
        }

        if (_comparer.Compare(item, _buffer[_count - 1]) <= 0)
        {
            return;
        }
        AddDescending(item);
    }

    public void AddDescending(T item)
    {
        int insertIndex = 0;
        while (_count < _buffer.Length && _comparer.Compare(item, _buffer[insertIndex]) < 0)
        {
            insertIndex++;
        }

        for (var i = Math.Min(_count, _buffer.Length - 1); i > insertIndex; i--)
        {
            _buffer[i] = _buffer[i - 1];
        }
        _buffer[insertIndex] = item;
        
    }

    public IEnumerable<T> Snapshot()
    {
        for(int i = 0 ; i < _count; i++)
        {
            yield return _buffer[i];
        }
    }
    
}

public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price}";
    }
}

internal class Comparer1 : IComparer<Product>
{
    public  int Compare(Product? x, Product? y)
    {
        if(x is null &&  y is null){return 0;}

        if (x == null)
        {
            return -1;
        }
        if (y == null){return 1;}

        return x.Name.Length.CompareTo(y.Name.Length);
    }
    
}