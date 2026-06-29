namespace ExercicesGenerics;

public class GenericPair<Tfirst,  Tsecond>
{
    public Tfirst First { get; set; }
    public Tsecond Second { get; set; }

    public GenericPair(Tfirst first, Tsecond second)
    {
        this.First = first;
        this.Second = second;
    }
    
    public static GenericPair<Tsecond,Tfirst> Swap(GenericPair<Tfirst,Tsecond> item)
    {
        var item1 = new GenericPair<Tsecond,Tfirst>(item.Second, item.First);
        return item1;
    }
    
    
    
}