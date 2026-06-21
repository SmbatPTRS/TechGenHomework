namespace JobSimulation;
using System.Collections;
using System.Collections.Generic;

public class JobQueueEnumerator : IEnumerator
{
    private Job[] jobs;
    private int Count;
    public int CurrentIndex = -1;
    
    public  JobQueueEnumerator(Job[] jobs, int count)
    {
        this.jobs = jobs;
        Count = count;
    }

    public bool MoveNext()
    {
        CurrentIndex++;
        while (CurrentIndex < Count && jobs[CurrentIndex].Status != JobStatus.Pending)
        {
            CurrentIndex++;
        }
        return CurrentIndex < Count;
    }

    public object Current
    {
        get
        {
            if (CurrentIndex >= Count || CurrentIndex < 0)
            {
                throw new InvalidOperationException();
            }
            return jobs[CurrentIndex];
        }
    }

    public void Reset()
    {
        CurrentIndex = -1;
    }
}

