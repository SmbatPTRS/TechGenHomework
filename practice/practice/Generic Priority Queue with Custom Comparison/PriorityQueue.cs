using System.Collections;
namespace practice.Generic_Priority_Queue_with_Custom_Comparison;

// In this code I used a generic built in class called Comparer<T>
//it's an abstract class that Microsoft already wrote inside .NET, 
//implements IComparer<T> for you, plus adds some convenient extra features.
// .Create(Comparison<T> comparison) and .Default

//the .Create(Comparison<T> comparison) takes the lambda method inside the delegate
// and turns it into an object of Comparer<T>, a class which implements IComparer<T> interface!


public class PriorityQueue<T>
{
    // our code basically functions via this _comparer
    private IComparer<T> _comparer;
    
    private readonly List<T> priorityQueue;

    public PriorityQueue()
    {
        //  Comparer<T>.Default checks if T implements IComparable<T> and uses it
        // otherwise InvalidOperationException
        this._comparer = Comparer<T>.Default;
    }
    
    public PriorityQueue(IComparer<T> comparer)
    {
        this._comparer = comparer;
    }

    public PriorityQueue(Comparison<T> comparison)
    {
        // here it just wrapped the Comparison<T> comparison delegate object 
        // into a full legit Comparer<T> object, and as it implements IComparer<T>
        // we can easily assign it to our _comparer
        // basically turned a lambda into a class object
        
       _comparer =  Comparer<T>.Create(comparison);
    }

    public void Enqueue(T item)
    {
        if (priorityQueue.Count == 0)
        {
            priorityQueue.Add(item);
            return;
        }

        int index = 0;
    
        // FIX 2: Removed the outer 'for' loop. You only need one search loop 
        // to find the target position where your new item belongs.
        while (index < priorityQueue.Count)
        {
            // Keep moving right as long as our new item has a lower priority
            if (_comparer.Compare(item, priorityQueue[index]) < 0)
            {
                break; // Stop! Found the insertion spot.
            }
            index++;
        }

        //Add a blank/default item to the very end of the list.
        priorityQueue.Add(default(T)!);

        // shift elements backwards (from right to left)!

        for (int j = priorityQueue.Count - 2; j >= index; j--)
        {
            priorityQueue[j + 1] = priorityQueue[j];
        }

        priorityQueue[index] = item;
    }

    public void Dequeue()
    {
        if (priorityQueue.Count == 0)
        {
            Console.WriteLine("Queue is empty");
        }
        else
        {
            priorityQueue.RemoveAt(0);
        }
    }



}