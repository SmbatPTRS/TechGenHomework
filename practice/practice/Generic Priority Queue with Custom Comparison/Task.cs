namespace practice.Generic_Priority_Queue_with_Custom_Comparison;

public class MyTask : IComparable<MyTask>
{
    public readonly string Name;
    private readonly int _priority;
    
    public MyTask(string name, int priority)
    {
        Name = name;
        this._priority = priority;
    }

    public int CompareTo(MyTask? obj)
    {
        return this._priority.CompareTo(obj?._priority);
    }

    public override string ToString()
    {
        return Name +  ": " + _priority + "\n";
    }
}