namespace practice.Generic_Priority_Queue_with_Custom_Comparison;

public class Task : IComparable<Task>
{
    public readonly string Name;
    private readonly int _priority;
    
    public Task(string name, int priority)
    {
        Name = name;
        this._priority = priority;
    }

    public int CompareTo(Task? obj)
    {
        return this._priority.CompareTo(obj?._priority);
    }
}