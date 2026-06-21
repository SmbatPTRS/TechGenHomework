namespace JobSimulation;
using System.Collections;
using System.Collections.Generic;


public class JobQueue : IEnumerable
{
    private Job[] jobs;
    private int Count { get; set; }
    public int Capacity{get;}   

    public JobQueue(int capacity)
    {
        if (capacity <= 0)
        {
            capacity = 4;
        }
        Capacity = capacity;
        jobs = new Job[capacity];
    }

    public void Enqueue(Job job)
    {
        if(Count  == jobs.Length)
        {
            Array.Resize(ref jobs, jobs.Length * 2);
        }
        jobs[Count] = job;
        Count++;
    }

    public IEnumerator GetEnumerator()
    {
        return new JobQueueEnumerator(jobs, Count);
    }
}